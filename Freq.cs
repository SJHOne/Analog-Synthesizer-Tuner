using System;
using System.Collections.Generic;
using System.Text;


namespace cswavrec
{
    class Freq
    {
        #region Attributes
        
        const int maxFilter  = 1024;
        const int filtLen    = 256; // 512 is pretty sharpish for 440Hz!
        const int filtFreq   = 440;
        const int sampleRate = 44100;
        const int bufferSize = 4096 * 2;
        const double M_PI = 3.14159265;
        short[,] bufs = new short[2, bufferSize];
        string[] scale = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
        int[] filter = new int[maxFilter];
        int filtArea;
        int b;
        
        #endregion

        #region Properties
        public short[] buffer2
        {
            get;
            private set;
        }
        public string NoteName
        {
            get;
            private set;
        }
        
        public int CentOffset
        {
            get;
            private set;
        }
        
        public bool BadSignal
        {
            get;
            private set;
        }
        public double Frequency
        {
            get;
            private set;
        }
        #endregion
        public int volSRC
        {
            get;
            private set;
        }
        public Freq()
        {
            //capturing = false; 
            b = 0;

            // Set up the filter
            filtArea = 0;
            for (int i = 0; i < filtLen; i++)
            {
                double profile = 512 * (1 + Math.Cos(M_PI * i / filtLen)) / 2;
                if (i == 0) profile /= 2;
                filter[i] = (int)(profile * Math.Cos((2 * M_PI * filtFreq * i) / sampleRate));
                filtArea += (int)profile;
            }
            filtArea /= 2;

            // make 'scope display white
            //TRect R; R.Top = 0; R.Left = 0; R.Bottom = 150; R.Right = 200;
            //Image1->Canvas->FillRect(R);

            //Image1->Canvas->MoveTo(0, 72 - 32); Image1->Canvas->LineTo(800, 72 - 32);
            //Image1->Canvas->MoveTo(0, 72 + 32); Image1->Canvas->LineTo(800, 72 + 32);
       
        }
        
        public void ProcessBuffer(short[] shortBuffer, int shortSize)
        {
            
            
            // Apply the filter
            
            buffer2 = new short[shortSize];
            for (int i = 0; i < shortSize; i++)
            {
                buffer2[i] = shortBuffer[i];
            }
            
            // Comment out this loop to bypass filtering /////////////////////////////
            /*
            for (int i=filtLen; i<(bufferSize-filtLen); i++)
            {
                int tot=0;
                for (int f=0; f<filtLen; f+=4)
                tot+=filter[f]*((int)buffer2[i+f]+buffer2[i-f])/2;
                bufs[b][i]=int(tot/(filtArea/4));
            }
            */
            ///////////////////////////////////////////////////////////////////////////

            // Now do the frequency-processing
            long vols=0;

            for (int n = 0; n < shortSize; n++)
            {
                vols+=(int)((buffer2[n])*buffer2[n]);    // added int() typecasting 20/07/2005
            }

            volSRC = (int)(Math.Sqrt(vols / shortSize));

            vols = 0;

            for (int n = filtLen; n < (shortSize - filtLen); n++)
                vols += (int)(shortBuffer[n] * shortBuffer[n]);    // added int() typecasting 20/07/2005

            int volume = (int)(Math.Sqrt( vols / (bufferSize-filtLen*2) ));

            int threshold = (int)(volume * 0.3);

            int state  = 1; 
            int firstT = -1;
            int endT   = 0; 
            int count  = -1;

            int[] wflip = new int [bufferSize/2];

            for (int n = filtLen; n < (bufferSize-filtLen); n++)
            {
                if ((state == 1) && (shortBuffer[n] < (-threshold))) 
                    state = -1;
                else
                {
                    if ((state == -1) && (shortBuffer[n] > threshold)) 
                    { 
                        state = 1; 
                        count++; 
                        endT = n; 
                        
                        if (count==0) 
                        { 
                            firstT = n; 
                        } 
                        
                        wflip[count]=n;  
                    }
                }
            }

            float freq;
            if ((endT != firstT) && (endT > 0))
                freq = (sampleRate*count) / (float)(endT-firstT);
            else
                freq = 0;

            bool okay = true;
            if (freq < 16)
            {
                okay = false;
            }
             int ave = 0;
             if (count > 0) 
                 ave=(endT - firstT)/count;

             int lim = 2 + ave/50;
             for (int n = 0; n < count; n++)
             {
                int t = (wflip[n+1] - wflip[n]) - ave; 
                
                if (t < 0) 
                    t = -t;
                
                if (t > lim) 
                    okay = false;
             }

            BadSignal = okay;
            
            if (volume > 16000) 
                BadSignal = true;

            //if (okay==FALSE) FreqLabel->Font->Color = (TColor)0x008000;
            //if (volume>16000) FreqLabel->Font->Color = (TColor)0x0000FF;

            Frequency = (double)freq * 2;

            if (okay)
            {
                int note = (int)(100*(48+(12*(Math.Log(freq/110)/Math.Log(2)))));

                NoteName = scale[((note + 50) / 100) % 12];

                int centoffs=((note+50)%100)-50;
                CentOffset = centoffs;
                
                //if (centoffs > 0) 
                //    CentOffset = "+" + CentOffset;

                //TrackBar1->Position = centoffs;
            }
            else
            {
                NoteName = string.Empty;
                CentOffset = 0;
            }

            // Scope Drawing Stuff ////////////////////////////////////////////

            //TRect R; R.Top=0; R.Left=0; R.Bottom=150; R.Right=200;
            //Image1->Canvas->FillRect(R);

            //Image1->Canvas->MoveTo(0, 72-32); Image1->Canvas->LineTo(800, 72-32);
            //Image1->Canvas->MoveTo(0, 72+32); Image1->Canvas->LineTo(800, 72+32);

            //if (volSRC == 0) volSRC = 1;   // added 20/07/2005
            
            //for (int i = 0; i < (800)/*bufferSize*/; i+=4)
            //{
                //if (i==0)
                    //Image1->Canvas->MoveTo(i/4, 72-(32*buffer2[i])/volSRC);
                //else
                    //Image1->Canvas->LineTo(i/4, 72-(32*buffer2[i])/volSRC);
            //}

            /*
             for (int i=0; i<bufferSize; i+=4)
             {
              if (i==0)
               Image1->Canvas->MoveTo(i/4, 72-(32*bufs[b][i])/volume);
              else
               Image1->Canvas->LineTo(i/4, 72-(32*bufs[b][i])/volume);
             }
            */

        //b = 1-b;
        }
    }
}
