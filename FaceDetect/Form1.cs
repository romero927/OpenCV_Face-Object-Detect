using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Dnn;
using OpenCvSharp.Extensions;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;
using OpenCvSharp.Aruco;

namespace DetectionApp
{
    public partial class MainForm : Form
    {
        private VideoCapture? capture;
        private Mat? frame;
        private CascadeClassifier? faceCascade;
        private Timer? timer;
        private bool processingFrame = false;

        // YOLO object detection
        private Net? yoloNet;
        private List<string>? yoloClasses;

        private enum DetectionMode
        {
            Face,
            Object
        }

        private DetectionMode currentMode = DetectionMode.Face;

        // Performance tracking
        private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        private Queue<double> processingTimes = new Queue<double>();
        private const int MaxProcessingTimeSamples = 30;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomControls();
            InitializeCamera();
            InitializeFaceDetection();
            InitializeObjectDetection();
            InitializeTimer();

            // Set form properties for better appearance
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "Face and Object Detection";
            this.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void InitializeCustomControls()
        {
            // Mode selector
            modeSelector.Items.AddRange(new object[] { "Face Detection", "Object Detection" });
            modeSelector.SelectedIndex = 0;
            modeSelector.SelectedIndexChanged += ModeSelector_SelectedIndexChanged;

            // Face detection controls
            scaleFactorTrackBar.Minimum = 11;
            scaleFactorTrackBar.Maximum = 20;
            scaleFactorTrackBar.Value = 11;
            scaleFactorTrackBar.ValueChanged += (s, e) => UpdateLabel(scaleFactorLabel, "Scale Factor", scaleFactorTrackBar.Value / 10.0);

            minNeighborsTrackBar.Minimum = 1;
            minNeighborsTrackBar.Maximum = 10;
            minNeighborsTrackBar.Value = 5;
            minNeighborsTrackBar.ValueChanged += (s, e) => UpdateLabel(minNeighborsLabel, "Min Neighbors", minNeighborsTrackBar.Value);

            minSizeTrackBar.Minimum = 10;
            minSizeTrackBar.Maximum = 100;
            minSizeTrackBar.Value = 30;
            minSizeTrackBar.ValueChanged += (s, e) => UpdateLabel(minSizeLabel, "Min Size", minSizeTrackBar.Value);

            UpdateLabel(scaleFactorLabel, "Scale Factor", scaleFactorTrackBar.Value / 10.0);
            UpdateLabel(minNeighborsLabel, "Min Neighbors", minNeighborsTrackBar.Value);
            UpdateLabel(minSizeLabel, "Min Size", minSizeTrackBar.Value);

            // Add a FPS counter
            fpsLabel.Text = "FPS: 0";

            // Add a confidence threshold slider for object detection
            confidenceThresholdLabel.Text = "Confidence Threshold: 0.5";
            confidenceThresholdTrackBar.Minimum = 1;
            confidenceThresholdTrackBar.Maximum = 100;
            confidenceThresholdTrackBar.Value = 50;
            confidenceThresholdTrackBar.TickFrequency = 10;
            confidenceThresholdTrackBar.ValueChanged += (s, e) =>
            {
                float threshold = confidenceThresholdTrackBar.Value / 100f;
                confidenceThresholdLabel.Text = $"Confidence Threshold: {threshold:F2}";
            };

            // Add a button to save the current frame
            saveFrameButton.Click += SaveFrameButton_Click;
        }

        private void ModeSelector_SelectedIndexChanged(object? sender, EventArgs e)
        {
            currentMode = modeSelector.SelectedIndex == 0 ? DetectionMode.Face : DetectionMode.Object;
            scaleFactorTrackBar.Enabled = minNeighborsTrackBar.Enabled = minSizeTrackBar.Enabled = (currentMode == DetectionMode.Face);
            confidenceThresholdTrackBar.Enabled = (currentMode == DetectionMode.Object);
            statusLabel.Text = $"Mode: {(currentMode == DetectionMode.Face ? "Face Detection" : "Object Detection")}";
        }

        private void InitializeCamera()
        {
            try
            {
                capture = new VideoCapture(0);
                if (!capture.IsOpened())
                {
                    throw new Exception("Unable to open camera.");
                }
                frame = new Mat();
                statusLabel.Text = "Camera initialized successfully.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing camera: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Camera initialization failed.";
                this.Close();
            }
        }

        private void InitializeFaceDetection()
        {
            try
            {
                faceCascade = new CascadeClassifier(".\\Models\\haarcascade_frontalface_default.xml");
                if (faceCascade.Empty())
                {
                    throw new Exception("Failed to load Haar Cascade classifier.");
                }
                statusLabel.Text = "Face detection initialized successfully.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing face detection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Face detection initialization failed.";
                this.Close();
            }
        }

        private void InitializeObjectDetection()
        {
            try
            {
                yoloNet = CvDnn.ReadNetFromDarknet(".\\Models\\yolov3.cfg", ".\\Models\\yolov3.weights");
                yoloClasses = File.ReadAllLines(".\\Models\\coco.names").ToList();
                statusLabel.Text = "Object detection initialized successfully.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing object detection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Object detection initialization failed.";
                this.Close();
            }
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 33; // Approximately 30 FPS
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async void Timer_Tick(object? sender, EventArgs e)
        {
            if (processingFrame) return;

            processingFrame = true;
            stopwatch.Restart();

            try
            {
                if (capture != null && capture.IsOpened())
                {
                    frame = new Mat();
                    capture.Read(frame);
                    if (!frame.Empty())
                    {
                        var detectionParams = GetDetectionParameters();
                        var processedFrame = await Task.Run(() => ProcessFrame(frame, detectionParams));
                        UpdatePictureBox(processedFrame);
                    }
                }
                UpdatePerformanceStats();
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Error: {ex.Message}";
            }
            finally
            {
                processingFrame = false;
            }
        }

        private void UpdatePerformanceStats()
        {
            stopwatch.Stop();
            double processingTime = stopwatch.Elapsed.TotalMilliseconds;

            processingTimes.Enqueue(processingTime);
            if (processingTimes.Count > MaxProcessingTimeSamples)
                processingTimes.Dequeue();

            double avgProcessingTime = processingTimes.Average();
            double fps = 1000.0 / avgProcessingTime;

            fpsLabel.Text = $"FPS: {fps:F1}";
            statusLabel.Text = $"Mode: {currentMode} | Avg. Processing Time: {avgProcessingTime:F1}ms";
        }

        private DetectionParameters GetDetectionParameters()
        {
            return new DetectionParameters
            {
                ScaleFactor = scaleFactorTrackBar.Value / 10.0,
                MinNeighbors = minNeighborsTrackBar.Value,
                MinSize = minSizeTrackBar.Value
            };
        }

        private Mat ProcessFrame(Mat frame, DetectionParameters parameters)
        {
            if (currentMode == DetectionMode.Face)
            {
                ProcessFaceDetection(frame, parameters);
            }
            else
            {
                ProcessObjectDetection(frame);
            }

            return frame;
        }

        private void UpdatePictureBox(Mat frame)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Mat>(UpdatePictureBox), frame);
                return;
            }

            pictureBox.Image?.Dispose();
            pictureBox.Image = BitmapConverter.ToBitmap(frame);
        }

        private void ProcessFaceDetection(Mat frame, DetectionParameters parameters)
        {
            if (faceCascade == null) return;

            using (var gray = new Mat())
            {
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
                Cv2.EqualizeHist(gray, gray);

                var faces = faceCascade.DetectMultiScale(
                    gray,
                    scaleFactor: parameters.ScaleFactor,
                    minNeighbors: parameters.MinNeighbors,
                    minSize: new OpenCvSharp.Size(parameters.MinSize, parameters.MinSize)
                );

                foreach (var face in faces)
                {
                    Cv2.Rectangle(frame, face, Scalar.Green, 2);

                    double confidence = CalculateFaceConfidence(gray, face);
                    string confidenceText = $"{confidence:F1}%";

                    Cv2.PutText(frame, confidenceText, new OpenCvSharp.Point(face.X, face.Y - 10),
                        HersheyFonts.HersheySimplex, 0.5, Scalar.Green);
                }
            }
        }

        private void ProcessObjectDetection(Mat frame)
        {
            if (yoloNet == null || yoloClasses == null) return;

            float confidenceThreshold = confidenceThresholdTrackBar.Value / 100f;

            using (var inputBlob = CvDnn.BlobFromImage(frame, 1 / 255f, new OpenCvSharp.Size(416, 416), new Scalar(), true, false))
            {
                yoloNet.SetInput(inputBlob);

                var outBlobNames = yoloNet.GetUnconnectedOutLayersNames();
                var outputBlobs = outBlobNames.Select(_ => new Mat()).ToArray();

                yoloNet.Forward(outputBlobs, outBlobNames.Select(n => n ?? string.Empty));

                var boxes = new List<Rect>();
                var confidences = new List<float>();
                var classIds = new List<int>();

                for (int i = 0; i < outputBlobs.Length; i++)
                {
                    var output = outputBlobs[i];
                    for (int j = 0; j < output.Rows; j++)
                    {
                        var scores = output.Row(j).ColRange(5, output.Cols);
                        Cv2.MinMaxLoc(scores, out _, out OpenCvSharp.Point classIdPoint);
                        var confidence = scores.At<float>(classIdPoint.X);
                        if (confidence > confidenceThreshold && yoloClasses[classIdPoint.X] != "person")
                        {
                            var centerX = (int)(output.At<float>(j, 0) * frame.Width);
                            var centerY = (int)(output.At<float>(j, 1) * frame.Height);
                            var width = (int)(output.At<float>(j, 2) * frame.Width);
                            var height = (int)(output.At<float>(j, 3) * frame.Height);
                            var left = centerX - width / 2;
                            var top = centerY - height / 2;

                            classIds.Add(classIdPoint.X);
                            confidences.Add(confidence);
                            boxes.Add(new Rect(left, top, width, height));
                        }
                    }
                }

                CvDnn.NMSBoxes(boxes, confidences, confidenceThreshold, 0.4f, out int[] indices);

                foreach (int i in indices)
                {
                    var box = boxes[i];
                    var classId = classIds[i];
                    var confidence = confidences[i];
                    DrawPred(frame, yoloClasses[classId], confidence, box.Left, box.Top, box.Right, box.Bottom);
                }
            }
        }

        private void DrawPred(Mat frame, string label, float conf, int left, int top, int right, int bottom)
        {
            Cv2.Rectangle(frame, new OpenCvSharp.Point(left, top), new OpenCvSharp.Point(right, bottom), Scalar.Red, 2);

            string labelWithConf = $"{label} {conf:P1}";
            var labelSize = Cv2.GetTextSize(labelWithConf, HersheyFonts.HersheySimplex, 0.5, 1, out int baseLine);
            top = Math.Max(top, labelSize.Height);
            Cv2.Rectangle(frame, new OpenCvSharp.Point(left, top - labelSize.Height),
                new OpenCvSharp.Point(left + labelSize.Width, top + baseLine), Scalar.Red, Cv2.FILLED);
            Cv2.PutText(frame, labelWithConf, new OpenCvSharp.Point(left, top), HersheyFonts.HersheySimplex, 0.5, Scalar.White);
        }

        private double CalculateFaceConfidence(Mat grayImage, Rect face)
        {
            using (var faceROI = new Mat(grayImage, face))
            {
                Cv2.MinMaxLoc(faceROI, out double minVal, out double maxVal);
                double contrast = maxVal - minVal;
                double size = Math.Sqrt(face.Width * face.Height);
                double aspectRatio = (double)face.Width / face.Height;

                double contrastFactor = Math.Min(contrast / 255.0, 1.0);
                double sizeFactor = Math.Min(size / 200.0, 1.0);
                double aspectRatioFactor = Math.Max(0, 1.0 - Math.Abs(aspectRatio - 1.3) / 0.5);

                return (contrastFactor * 0.4 + sizeFactor * 0.4 + aspectRatioFactor * 0.2) * 100.0;
            }
        }

        private void UpdateLabel(Label label, string prefix, double value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Label, string, double>(UpdateLabel), label, prefix, value);
                return;
            }

            label.Text = $"{prefix}: {value:F2}";
        }

        private void SaveFrameButton_Click(object? sender, EventArgs e)
        {
            if (frame == null || frame.Empty()) return;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
                saveFileDialog.Title = "Save Frame";
                saveFileDialog.ShowDialog();

                if (!string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    frame.SaveImage(saveFileDialog.FileName);
                    MessageBox.Show("Frame saved successfully!", "Save Frame", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            timer?.Stop();
            capture?.Dispose();
            frame?.Dispose();
            faceCascade?.Dispose();
            yoloNet?.Dispose();
        }

        private class DetectionParameters
        {
            public double ScaleFactor { get; set; }
            public int MinNeighbors { get; set; }
            public int MinSize { get; set; }
        }
    }
}