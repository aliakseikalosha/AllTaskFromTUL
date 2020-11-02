clc;clear all; close all;

Fs = 1000;
f = 2;
n = 10000;
t = 0:1/Fs:1-1/Fs;
x1 = zeros(size(t));
x2 = zeros(size(t));
gfc = figure();
for i=1:n
    x2 = x2 + (1/i)*cos(2*pi*i*f.*t - pi/2);
    j = i*2-1;
    x1 = x1 + (1/j)*cos(2*pi*j*f.*t - pi/2);
end
draw(x1,t,2,2,1,Fs,1000);
draw(x2,t,2,2,3,Fs,1000);
subplot(2,2,1);
saveas(gcf, 'output.png');

function draw(x,t,s1,s2,i,Fs,n)
    subplot(s1,s2,i);
    plot(t,x)
    ylabel('A');
    xlabel('t[s]');
    F = 0:Fs/n:Fs/2-Fs/n;
    X = fft(x,n);
    subplot(s1,s2,i+1);
    stem(F(1:40),1/(n/2)*abs(X(1:40)),'.');
    ylabel('|A|');
    xlabel('f[Hz]');
end