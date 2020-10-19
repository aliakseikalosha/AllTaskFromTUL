function sample = musicSoundSquare(volume,t,f)
    %MUSICSOUNDSIN Summary of this function goes here
    %   Detailed explanation goes here
    sample = volume.*square(2*pi*t*f);
    sample = sample + rand(1,size(t,2))*f*0.0001 + volume*0.2.*sawtooth(2*pi*t*f*1.001); 
    sample = sample./max(sample);
end
