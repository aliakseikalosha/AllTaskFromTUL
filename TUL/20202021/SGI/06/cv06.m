clc;clear all;close all;

[x,Fs] = audioread("ovcaci-housle.wav");
w = 255;
o = 128;
j = 0;
for i = 1:o:size(x,1)-o
    j = j+1;
    ZCR(j) = zeroCrossingRate(x(i:min(i+w,size(x,1))));
end

subplot(3,1,1);
plot(x)
subplot(3,1,2);
plot(ZCR);

function c = zeroCrossingRate(x)
    N = size(x,1)-1;
    sum = 0;
    for i=2:N
        if sign(x(i)) ~= sign(x(i-1))
            sum = sum + 2;
        end
    end
    c = 1/(2*(N-1))*sum;
end