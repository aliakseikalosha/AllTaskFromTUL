clc
clear global
close all
Fs = 20;
T = 2;
t = 0:1/Fs:T-1/Fs;
x = graf(2,t,4,pi/3,0);
y = graf(4,t,2,pi/4,0);
subplot(3,1,1)
stem(t,x)
subplot(3,1,2)
stem(t,y)
subplot(3,1,3)
stem(t,x+y)

function r = graf(A,t,f,fi,M)
    r = A*cos(2*pi*t*f+fi)+M;
end
