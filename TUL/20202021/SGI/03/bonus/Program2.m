clc;clear all; close all;

fileID = fopen('FilesList.txt','r');
textdata = textscan(fileID,'%s');  
fclose(fileID);
fileNames = string(textdata{:}); 
numFiles = size (fileNames, 1);
success = 0;

for i = 1:numFiles
    fileIsJ = fileNameisJ(fileNames(i));
    [x,Fs] = audioread(fileNames(i));
    de=10/1000*Fs;
    x = x./max(x);
    en = [];
    for k = 1:de:Fs-de
        en =[en sum(x(k:k+de).^2)];
    end
    en = removeNoise(en);
    
    if isJ(en) == fileIsJ
        success = success+1;
        fprintf("Success. %s\n",fileNames(i));
    else
        fprintf("Fail. %s\n",fileNames(i));
    end
end

fprintf("success rate : %d%%\n", success/numFiles*100);

function result = fileNameisJ(name)
    result = false;
    if max(size(regexp(name,'.*J[0-9]*\.wav'))) > 0
        result = true;
    end
end

function  guess = isJ(energy)
    guess = false;
    [n,m] = size(energy);
    count = 0;
    spikeStart = false;
    for i = 1:m
        if(energy(i)~= 0) & (~spikeStart)
            count = count + 1;
            spikeStart = true;
        elseif (energy(i) == 0) & spikeStart
            spikeStart = false;
        end
    end
    guess = count == 1; 
end

function x = removeNoise(s)
    x = s./max(s);
    %x(x<0.2) = 0;
    for i=3:size(x,2)-2
%         if (x(i-1) == 0) & (x(i) ~= 0) & (x(i+1) == 0)
%             x(i)=0;
%         end
        x(i) = (x(i-2)+x(i-1)+x(i+1)+x(i+2))/4;
    end
    x(x<0.2) = 0;
end

    