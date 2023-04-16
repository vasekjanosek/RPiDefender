using Unosquare.RaspberryIO;

namespace RPiDefender.Data
{
    public class CameraService
    {

        public CameraService() { }
        public async Task<byte[]> MakePicture()
        {
            if (Pi.Info.RaspberryPiVersion == Unosquare.RaspberryIO.Computer.PiVersion.Unknown)
            {
                return null;
            }
            return await Pi.Camera.CaptureImageJpegAsync(1920, 1080);
        }
    }
}
