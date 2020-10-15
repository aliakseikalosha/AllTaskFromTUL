clc;clear global; close all;

TEST = false;
PLAY_SOUND = false;

if TEST
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
success = 0;

for i = 1:numFiles
    fileIsJ = fileNameisJ(fileNames(i));
    [x,Fs] = audioread(fileNames(i));
    de=10/1000*Fs;
    x = x(1:Fs);
    x = x./max(x);
    if TEST == TEST
        if fileIsJ 
            subplot(size(fileNames,1)/2,2,j);
            j= j+2; 
        else
            subplot(size(fileNames,1)/2,2,z);
            z = z+2;
        end
    end
    en = [];
    for k = 1:de:Fs-de
        en =[en sum(x(k:k+de).^2)];
    end
    den = zeros(1,size(en,2)-1);
    for k = 1:size(den,2)
        den(k) = en(k) - en(k+1);
    end
    en = removeNoise(en);
    plot(en);
    %title(fileNames(i));
    if isJ(en) == fileIsJ
        success = success+1;
        fprintf("Success. %s\n",fileNames(i));
    else
        fprintf("Fail. %s\n",fileNames(i));
    end
    if PLAY_SOUND
        sound (x, Fs);
        pause(1);
    end
end

fprintf("success rate : %d%%\n", success/size(fileNames,1)*100);

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

    