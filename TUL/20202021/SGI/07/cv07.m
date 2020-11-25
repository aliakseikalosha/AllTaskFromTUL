clc;close all;clear all;
Fs=10;
t=0:1/Fs:2-1/Fs;
f1=cos(2*pi*4*t);
f2=cos(2*pi*2.5*t);
f3=cos(2*pi*12*t);
f4=cos(2*pi*7.25*t);

X1_10 = DTF(f1,10);
X2_10 = DTF(f2,10);
X3_10 = DTF(f3,10);
X4_10 = DTF(f4,10);

X1_20 = DTF(f1,20);
X2_20 = DTF(f2,20);
X3_20 = DTF(f3,20);
X4_20 = DTF(f4,20);

d=Fs/10;
subplot(4,2,1)
stem(0:d:Fs-d,abs(X1_10))
subplot(4,2,3)
stem(0:d:Fs-d,abs(X2_10))
subplot(4,2,5)
stem(0:d:Fs-d,abs(X3_10))
subplot(4,2,7)
stem(0:d:Fs-d,abs(X4_10))

d=Fs/20;
subplot(4,2,2)
stem(0:d:Fs-d,abs(X1_20))
subplot(4,2,4)
stem(0:d:Fs-d,abs(X2_20))
subplot(4,2,6)
stem(0:d:Fs-d,abs(X3_20))
subplot(4,2,8)
stem(0:d:Fs-d,abs(X4_20))
saveas(gcf, 'output.png')

function X = DTF(x,N)
    X = zeros(1,N);
    for k=0:N-1
        for n=0:N-1 
            X(k+1) = X(k+1) + x(n+1).*exp(-(1i*2*pi*n*k)/N);
        end
    end
    X=X.*(1/N);
end