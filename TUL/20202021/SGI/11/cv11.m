close all; clear all; clc;
Fs = 1000;
t = 0:1/Fs:1-1/Fs;
x = cos(100*2*pi*t)+cos(200*2*pi*t)+cos(300*2*pi*t)+cos(400*2*pi*t);
X = fft(x,Fs);
subplot(2,1,1)
drawSpektrum('Spektrum signálu x', X,Fs);
B = [1 -0.61803 1];
A = 1;
y = filter(B,A,x);
Y = fft(y,Fs);
subplot(2,1,2)
drawSpektrum('Spektrum signálu y', Y,Fs);
saveas(gcf, 'output.png')

function drawSpektrum(t, X, Fs)
stem(0:Fs-1,abs(X)/Fs);
title(t);
xlabel('f[Hz]');
ylabel('|A|');
end

