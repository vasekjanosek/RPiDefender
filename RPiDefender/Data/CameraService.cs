using Unosquare.RaspberryIO;

namespace RPiDefender.Data
{
    public class CameraService
    {
        public async Task<byte[]> MakePicture()
        {
            return await Pi.Camera.CaptureImageJpegAsync(1920, 1080);
        }
    }
}
