Fs = 1000
t = 0:1/Fs:T-1/Fs;
x = zeros(size(t));
for f=1:4
    x=x+cos(2*pi*(f*100).*t);
end
fir