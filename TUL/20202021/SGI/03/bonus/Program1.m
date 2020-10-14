clc;clear global; close all;

MY_FILES = true;
PLAY_SOUND = false;

if MY_FILES
    fileID = fopen('learnFiles.txt','r');
else
    fileID = fopen('FilesList.txt','r');
end
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
    x = x(1:Fs);
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
    den = zeros(1,size(en,2)-1);
    for k = 1:size(den,2)
        den(k) = en(k) - en(k+1);
    end
    en = en./max(en);
    plot (en)%(den./max(den));
    %title(fileNames(i));
    if PLAY_SOUND
        sound (x, Fs);
        pause(1);
    end
end
