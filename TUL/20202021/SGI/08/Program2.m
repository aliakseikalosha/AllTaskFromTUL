clear all;clc;close all
for personNumber = 3:3
    for fileNumber = 1:5
        for number = 1:10
            [x,Fs] = audioread(join([sprintf("Osoba%d/c%d_pjjjj_s0%d",personNumber,number-1,fileNumber),".wav"],''));
            de=25/1000*Fs;
            subplot(10,3,(number-1)*3+1)
            plot(x)
            subplot(10,3,(number-1)*3+2)
            plot(energy(x,de))
            subplot(10,3,(number-1)*3+3)
            n=80;
            m=0;
            nfft=256;
            spectrogram(x,n,m,nfft,Fs,'yaxis') 
        end
        set(gcf, 'Name', sprintf("person %d session %d", personNumber, fileNumber));
        pause
    end
end

function e = energy(x,de)
    e=[];
    for i=1:de:length(x)-de
        e = [e sum(x(i:i+de).^2)];
    end
end