# ImageConverter

ImageConverter is a C# console application that resizes images (JPEG and PNG) from a specified folder to a user-defined width and height.

## Prerequisites

- .NET SDK 8.0 or later. (The project was built using .NET 8.0)

## How to Run

1.  Open a terminal or command prompt.
2.  Navigate to the root directory of the `ImageConverter` project (the directory containing `ImageConverter.csproj`).
3.  Compile and run the application using the following command:
    ```bash
    dotnet run --project ImageConverter
    ```
    Alternatively, if you are already in the `ImageConverter` directory:
    ```bash
    dotnet run
    ```

## Usage

When you run the application, you will be prompted for the following:

1.  **Path to the folder containing images**: Enter the full path to the directory where your images are located.
    - The application will validate if the path exists.
2.  **Desired width**: Enter the target width for the resized images (e.g., `800`).
    - Must be a positive integer.
3.  **Desired height**: Enter the target height for the resized images (e.g., `600`).
    - Must be a positive integer.
4.  **Confirmation**: You will be shown the chosen resolution and asked to confirm (Y/N). If you enter 'N' or 'n', you will be prompted for the width and height again.

**Supported Image Types**:
- JPEG (`.jpg`, `.jpeg`)
- PNG (`.png`)

Resized images will be saved in a new subdirectory named `output_images`, which will be created inside the input folder you provided. Original images will not be modified.

## Error Handling

- The application includes basic error handling for:
    - Invalid or non-existent input folder paths.
    - Non-integer or non-positive values for width and height.
    - Issues encountered during the processing of individual image files (e.g., corrupted file, unsupported format not caught by the initial filter). If an error occurs with one file, a message will be displayed, and the application will attempt to process the next file.
