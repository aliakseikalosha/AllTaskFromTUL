function sample = musicSoundBetterSin(volume,t,f)
    %MUSICSOUNDSIN Summary of this function goes here
    %   Detailed explanation goes here
    s = size(t,2);
    f = [linspace(0,f,s*0.01),linspace(f,f-f*0.05,s-s*0.01)];
   % f = f+sin(pi*t.*(f./10));
    volume = [linspace(0,volume,s*0.01),linspace(volume,volume*0.9,s-s*0.01)];
    sample = volume.*cos(2*pi*t.*f(1)) + volume.*0.1.*cos(2*pi*t.*f);
end



