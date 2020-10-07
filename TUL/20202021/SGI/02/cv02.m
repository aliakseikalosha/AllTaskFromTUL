clc
clear global
close all
Fs = 20;
T = 2;
t = 0:1/Fs:T-1/Fs;
sig1 = graf(2,t,4,pi/3,0);
sig2 = graf(4,t,2,pi/4,0);
gcf = figure();
subplot(3,1,1)
stem(t,sig1)
subplot(3,1,2)
stem(t,sig2)
subplot(3,1,3)
sig12 = sig1 + sig2;
stem(t,sig12)
saveas(gcf, 'output.png')

function r = graf(A,t,f,fi,M)
    r = A*cos(2*pi*t*f+fi)+M;
end
