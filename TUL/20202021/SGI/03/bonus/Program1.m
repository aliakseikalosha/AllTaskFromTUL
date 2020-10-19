clc;clear all; close all;

PLAY_SOUND = false;

fileID = fopen('learnFiles.txt','r');
textdata = textscan(fileID,'%s');  
fclose(fileID); 
fileNames = string(textdata{:});
numFiles = size (fileNames, 1);

j = 1;
z = 2;

for i = 1:numFiles
    isJ = regexp(fileNames(i),'.*J[0-9]*\.wav');
    [x,Fs] = audioread(fileNames(i));
    de=10/1000*Fs;
   if isJ == 1
        subplot(size(fileNames,1)/2,2,j);
        j= j+2; 
    else
        subplot(size(fileNames,1)/2,2,z);
        z = z+2;
    end
    en = [];
    for k = 1:de:Fs-de
        en =[en sum(x(k:k+de).^2)];
    end
    plot (en)
    if PLAY_SOUND
        sound (x, Fs);
        pause(1);
    end
end
