clc;close all;clear all;
for fileNumber = 1:1%4
    [x,fs] = audioread(sprintf("10%d.wav",fileNumber));
    subplot(2,1,1)
    plot(energy(x,25/1000*fs))
    subplot(2,1,2)
    plot(x)
end

function e=energy(x,de)
    e=[];
    for i=1:de:length(x)-de
        e = [e sum(x(i:i+de).^2)];
    end
end