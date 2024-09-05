# Face and Object Detection Application

This Windows Forms application uses OpenCV to perform real-time face detection and object detection using a webcam feed. It provides a user-friendly interface for switching between detection modes and adjusting various parameters.

## Features

- Real-time face detection using Haar Cascade classifier
- Object detection using YOLO (You Only Look Once) v3
- Switch between face detection and object detection modes
- Adjustable parameters for face detection (scale factor, min neighbors, min size)
- Adjustable confidence threshold for object detection
- FPS (Frames Per Second) counter
- Ability to save the current frame as an image file
- Status display showing current mode and processing time

## Prerequisites

Before you begin, ensure you have met the following requirements:

- Windows operating system
- .NET Framework 4.7.2 or later
- Visual Studio 2019 or later
- Webcam connected to your computer

## Setup

1. Clone this repository or download the source code.
2. Open the solution in Visual Studio.
3. Restore NuGet packages. This project requires:
   - OpenCvSharp4
   - OpenCvSharp4.runtime.win
4. Download the following files and place them in the project's output directory (usually `bin/Debug` or `bin/Release`):
   - `haarcascade_frontalface_default.xml` (for face detection)
     - Download from: https://github.com/opencv/opencv/blob/master/data/haarcascades/haarcascade_frontalface_default.xml
   - `yolov3.cfg` (YOLO configuration file)
     - Download from: https://github.com/pjreddie/darknet/blob/master/cfg/yolov3.cfg
   - `yolov3.weights` (YOLO pre-trained weights)
     - Download from: https://pjreddie.com/media/files/yolov3.weights
   - `coco.names` (COCO dataset class names)
     - Download from: https://github.com/pjreddie/darknet/blob/master/data/coco.names

## Usage

1. Build and run the application in Visual Studio.
2. The application will attempt to initialize your webcam and the detection models.
3. Use the mode selector dropdown to switch between Face Detection and Object Detection.
4. Adjust the parameters using the provided sliders:
   - For Face Detection: Scale Factor, Min Neighbors, and Min Size
   - For Object Detection: Confidence Threshold
5. The main window shows the webcam feed with detected faces or objects outlined.
6. The status bar at the bottom shows the current mode, processing time, and FPS.
7. To save the current frame, click the "Save Frame" button and choose a location to save the image.

## Troubleshooting

- If the camera fails to initialize, ensure that your webcam is properly connected and not being used by another application.
- If detection models fail to load, verify that you have downloaded all required files and placed them in the application's directory.
- For performance issues, try adjusting the parameters (increasing the minimum face size or confidence threshold) to reduce the processing load.

## Contributing

Contributions to this project are welcome. Please fork the repository and submit a pull request with your changes.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- OpenCV for the computer vision algorithms
- OpenCvSharp for the .NET wrapper of OpenCV
- YOLO (You Only Look Once) for the object detection model