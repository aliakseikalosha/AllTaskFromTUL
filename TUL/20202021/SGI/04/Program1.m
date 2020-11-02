clc;clear all;close all;
fileID = fopen('filesList.txt','r');
textdata = textscan(fileID,'%s');  
fclose(fileID);
fileNames = string(textdata{:});
numFiles = size (fileNames, 1);
n=80;
m=0;
nfft=256;
for i = 1:numFiles   
    [x,Fs] = audioread(fileNames(i));
    g = figure();
    %subplot(5,2,i)
    spectrogram(x,n,m,nfft,Fs,'yaxis') 
    title(fileNames(i));
    audiowrite(fileNames(i),x(1:32000),Fs);
    sound(x,Fs);
    saveas(g,sprintf("c%d.png",i-1));
end