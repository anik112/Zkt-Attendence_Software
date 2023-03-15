using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AForge.Video;
using AForge.Video.DirectShow;


namespace CAMERAPROJECT
{
    public partial class CameraView : Form
    {
        public int islemdurumu = 0; //CAMERA STATUS
        FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        VideoCaptureDevice videoSource = null;
        public static int durdur = 0;
        public static int gondermesayisi = 0;
        public int kamerabaslat = 0;
        public int selected = 0;
        public CameraView()
        {
            InitializeComponent();
        }

        

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            imgVideo.Image = resizeImage(img, new Size(80, 100)); 
        }

        private void CAPTURE_Click(object sender, EventArgs e)
        {

            
                if (imgVideo != null) {
                imgCapture.Image = resizeImage (imgVideo.Image, new Size(80, 100)) ; 
                }
             
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
         

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.label1.Text = "";
                //Enumerate all video input devices
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    label1.Text = "No local capture devices";
                }
                foreach (FilterInfo device in videoDevices)
                {
                    int i = 1;
                    comboBox1.Items.Add(device.Name);
                    label1.Text = ("camera" + i + "initialization completed..." + "\n");
                    i++;
                }
                comboBox1.SelectedIndex = 0;
            }
            catch (ApplicationException)
            {
                this.label1.Text = "No local capture devices";
                videoDevices = null;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                videoSource.SignalToStop();
                videoSource = null;
                if (!(videoSource == null))
                {
                    videoSource.Stop();
                    videoSource = null;
                }
            }
            catch { }
            Application.Exit();

        }

        private void SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                imgCapture.Image = imgVideo.Image;
            }
            catch { }
            /* kaydet butonu  bntSave_Click*/
            try
            {

                imgCapture.Image.Save(@"resim.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
              
                 label1.Text = "IMAGE SAVED";
               
            }
            catch { }
        }

        private void startCamaraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selected = comboBox1.SelectedIndex;

            if (islemdurumu == 0)
            {


                if (kamerabaslat > 0) return;
                try
                {
                    videoSource = new VideoCaptureDevice(videoDevices[selected].MonikerString);
                    videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                    videoSource.Start(); kamerabaslat = 1; //CAMERA STARTRED

                }
                catch
                {
                    MessageBox.Show("RESTART THE PROGRAM");

                    if (!(videoSource == null))
                        if (videoSource.IsRunning)
                        {
                            videoSource.SignalToStop();
                            videoSource = null;
                        }
                }//catch
            }
        }

        private void resetCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(videoSource == null))
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }

            kamerabaslat = 0;
            imgVideo.Image = null;

            label1.Text = "CAMERA TURN OFF";
        }

        private void pushToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!(videoSource == null))
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                videoSource.SignalToStop();
                videoSource = null;
                if (!(videoSource == null))
                {
                    videoSource.Stop();
                    videoSource = null;
                }
            }
            catch { }
            Application.Exit();
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void imgVideo_Click(object sender, EventArgs e)
        {

        }

        ///
    }
}
