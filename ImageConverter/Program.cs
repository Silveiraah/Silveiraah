using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

﻿// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string? directoryPath;
do
{
    Console.WriteLine("Enter the path to the folder containing the images:");
    directoryPath = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(directoryPath) || !Directory.Exists(directoryPath))
    {
        Console.WriteLine("Invalid directory path. Please try again.");
        directoryPath = null; // Reset to ensure loop continues
    }
} while (directoryPath == null);

Console.WriteLine($"Selected directory: {directoryPath}");

int desiredWidth;
int desiredHeight;
bool dimensionsConfirmed = false;

do
{
    // Get desired width
    do
    {
        Console.WriteLine("Enter the desired width for the images (e.g., 800):");
        string? widthInput = Console.ReadLine();
        if (int.TryParse(widthInput, out desiredWidth) && desiredWidth > 0)
        {
            break;
        }
        Console.WriteLine("Invalid width. Please enter a positive integer.");
    } while (true);

    // Get desired height
    do
    {
        Console.WriteLine("Enter the desired height for the images (e.g., 600):");
        string? heightInput = Console.ReadLine();
        if (int.TryParse(heightInput, out desiredHeight) && desiredHeight > 0)
        {
            break;
        }
        Console.WriteLine("Invalid height. Please enter a positive integer.");
    } while (true);

    Console.WriteLine($"You chose to resize images to {desiredWidth}x{desiredHeight}. Confirm? (Y/N)");
    string? confirmation = Console.ReadLine();
    if (confirmation != null && confirmation.Trim().Equals("Y", StringComparison.OrdinalIgnoreCase))
    {
        dimensionsConfirmed = true;
    }
    else
    {
        Console.WriteLine("Dimensions not confirmed. Please enter again.");
    }
} while (!dimensionsConfirmed);

Console.WriteLine($"Images will be resized to {desiredWidth}x{desiredHeight}.");

string outputDirectoryPath = Path.Combine(directoryPath, "output_images");
Directory.CreateDirectory(outputDirectoryPath);

Console.WriteLine($"Output directory created at: {outputDirectoryPath}");

string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
string[] imageFiles = Directory.GetFiles(directoryPath)
    .Where(file => allowedExtensions.Contains(Path.GetExtension(file).ToLowerInvariant()))
    .ToArray();

if (imageFiles.Length == 0)
{
    Console.WriteLine("No images found in the specified directory with .jpg, .jpeg, or .png extensions.");
}
else
{
    Console.WriteLine($"Found {imageFiles.Length} image(s) to process.");
    foreach (string imagePath in imageFiles)
    {
        string fileName = Path.GetFileName(imagePath);
        string outputFilePath = Path.Combine(outputDirectoryPath, fileName);
        Console.WriteLine($"Processing {fileName}...");

        try
        {
            using (Image originalImage = Image.FromFile(imagePath))
            using (Bitmap resizedBitmap = new Bitmap(originalImage, new Size(desiredWidth, desiredHeight)))
            {
                ImageFormat format = Path.GetExtension(fileName).ToLowerInvariant() switch
                {
                    ".png" => ImageFormat.Png,
                    _ => ImageFormat.Jpeg,
                };
                resizedBitmap.Save(outputFilePath, format);
                Console.WriteLine($"Saved resized image to {outputFilePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing {fileName}: {ex.Message}");
            // Optionally, log the full exception details
        }
    }
    Console.WriteLine("Image processing complete.");
}

// Keep the console window open for testing
Console.ReadLine();
