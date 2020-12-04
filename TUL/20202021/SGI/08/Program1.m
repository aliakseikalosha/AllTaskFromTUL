clear all;clc;close all
for personNumber = 3:3
    for fileNumber = 5:5
        [x,Fs] = audioread(sprintf('Osoba%d/cv08_0%d.wav',personNumber,fileNumber));
        de=25/1000*Fs;
        e = energy(x,de);
        ix = 0;
        foundNumbers = 0;
        plot(e)
        title(sprintf("person %d session %d", personNumber, fileNumber))
        while(foundNumbers<10)
            ix=ix+1;
            if(e(ix)>1)
                foundNumbers = foundNumbers + 1;
                number = x(ix*de-1*Fs:ix*de+1*Fs-1);
                xline(ix-1*Fs/de);
                xline(ix+1*Fs/de);
                audiowrite(sprintf("Osoba%d/c%d_pjjjj_s0%d.wav",personNumber,mod(foundNumbers,10),fileNumber),number,Fs);
                ix = ix+1*Fs/de;
                pause
            end
        end
    end
end

function e = energy(x,de)
    e=[];
    for i=1:de:length(x)-de
        e = [e sum(x(i:i+de).^2)];
    end
end