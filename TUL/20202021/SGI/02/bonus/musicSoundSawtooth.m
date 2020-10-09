function sample = musicSoundSawtooth(volume,t,f)
    %MUSICSOUNDSIN Summary of this function goes here
    %   Detailed explanation goes here
    sample = volume*sawtooth(2*pi*t*f,1/2);
end


