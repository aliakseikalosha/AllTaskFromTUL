clc; close all;clear all;
seq = "1234567890";
Fs = 8000;
t = 0:1/Fs:0.1-1/Fs;
seq = convertStringsToChars(seq);
x = [];
for i=1:size(seq,2)
    x=[x,tone(seq(i),t),zeros(1,floor((100+floor(rand()*50)))*Fs/1000)];
end
x=x+0.05*rand(size(x));
x=x./max(x);
audiowrite(sprintf("dtmf_%s.wav",seq),x,Fs);
function x = tone(n,t)
    row = [697,770,852,941];
    collums = [1209,1336,1477];
    numPad = ['1','2','3';'4','5','6';'7','8','9';'*','0','#';];
    [r,c] = find(numPad==n);
    x = wave(row(r),t)+wave(collums(c),t);
end

function x = wave(f,t)
    x = cos(2*pi*f.*t);
end
