# PAIA 9700 Analog Synthesizer Calibration Visualizer

![Screenshot](/Pictures/UI.gif)

The PAIA 9700s is a compact analog modular music synthesizer, available as a kit. It took me about 3 weeks to get everything constructed and installed in the case.

After building the next task is to set the scale trimmers on both voltage controlled oscillators so that they stay in tune over the largest range of musical octaves. This process involves going “back and forth” between the scale pot and front tuning knob until you get both oscillators tuned. It’s a difficult task as it’s not always obvious how the oscillator tracking is moving around when you alter the scale.

I found that the best way to set the scale is to tune the root note to ‘C’ then play one octave above and note how much shift there is in the upper C. If the shift is positive (sharp) then actually set the scale to make it sharper (counter intuitive I know) then drop back down to the lower C and use the front panel pitch control to move the whole range down.

On my PAIA 9700s page I mentioned a neat tuning utility that could help with the process. I contacted the author; Andrew Steer, as I was hoping to incorporate his source code into something I was creating to help tune my PAIA 9700s synthesizer. He sent me the routine that detects pitch – it was in Borland C++, but it turned out not to be too difficult to replicate the code in C#. I added MIDI support and a graphing function to assist in the tuning.

Now all you need to do is connect the first oscillator output to your mic in, click “Start” to see the signal in the oscilloscope window, and adjust the mic input level to get a clean signal.

Then you click start test, and it will send MIDI data out of the default MIDI port (which you should connect to the input of the 9700) and read the frequency output. 
Once it has completed a pass of oscillator one, it will prompt you to connect up oscillator two, and repeats the test. Finally it plots a graph of the two oscillator responses against an ideal curve. (Log plot is also available).

In this way you can see the changes as you make them.
