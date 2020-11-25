clc;clear all;close all;

[x,Fs] = audioread("ovcaci-housle.wav");
w = 256;
o = 128;
j = 0;
for i = 1:o:size(x,1)-o
    j = j+1;
    ZCR(j) = zeroCrossingRate(x(i:min(i+w,size(x,1))));
end
if(i~=size(x,1))
    ZCR(end) = ZCR(end)*(size(x,1)-i)/w;
end
subplot(3,1,1);
plot(x)
subplot(3,1,2);
plot(ZCR);
w=myHann(256);
saveas(gcf, 'output.png')
function h = myHann(n)
    h=zeros(1,n-1);
    for i=0:n-1
        h(i+1) = (1-cos(2*pi.*i/(n-1)))/2;
    end
    h=h';
end
function c = zeroCrossingRate(x)
    N = size(x,1)-1;
    s = 0;
    for i=2:N
        s = s + abs(sign(x(i))-sign(x(i-1)));
    end
    c = 1/(2*(N-1))*s;
end