clc; close all;clear all;
fileID = fopen('FilesList.txt','r');
textdata = textscan(fileID,'%s');  
fclose(fileID);
fileNames = string(textdata{:});
numFiles = size (fileNames, 1);
score=0;
fileID = fopen('Protokol_Kalosha.txt','w');
for i=1:numFiles
    
    [x,Fs ] = audioread(fileNames(i));
    w = 256;
    l = size(x,1);
    s = [];
    index = 0;
    
    for j=1:w:size(x,1)-w
        dx = x(j:j+w);
        Y = fft(dx.*hamming(w+1));
        P2 = abs(Y/w);
        P1 = P2(1:w/2+1);
        P1(2:end-1) = 2*P1(2:end-1);

        f = Fs*(0:(w/2))/w;
        [f1,f2] = dialFreq(f,P1);
        guess = freq2num(f1,f2);
        if((size(s,1)==0) | ( s(index)~=guess))
            index = index+1;
            s(index) = guess; 
        end
    end
    
    s= s(s~='-');
    name = sprintf("dtmf_%s.wav",char(s));
    fprintf("%30s\t%10s\n", fileNames(i),s);
    fprintf(fileID,"%30s\t%10s\n", fileNames(i),s);
    if sameName(fileNames(i), name)
        score= score + 1;
    end
    
end
fprintf("%3.2d%%\n", score/max(size(fileNames))*100);
fprintf(fileID,"%3.2d%%\n", score/max(size(fileNames))*100);
fclose(fileID);

function b=sameName(fileName, guess)
    charFileName = convertStringsToChars(fileName);
    b=charFileName(end+1-max(strlength(guess)):end) == guess; 
end

function n=freq2num(fr,fc)
    row = [697,770,852,941];
    collums = [1209,1336,1477];
    numPad = ['1','2','3';'4','5','6';'7','8','9';'*','0','#';];
    n='-';
    r = find(row==fr);
    c = find(collums==fc);
    if (min(size(c))~=0)&(min(size(r))~=0)
        n = numPad(r,c);
    end
end

function [x1,x2] = dialFreq(f,p)
    [s,I] = sort(p);
    sFeq = f(I);
    f1=closest(sFeq(end));
    f2=closest(sFeq(end-1));
    x1=min([f1,f2]);
    x2=max([f1,f2]);
    if (x1==-1)|(x2==-1)
        x1 = -1;
        x2 = -1;
    end
end

function f=closest(n)
    maxOffset = 30;
    f = -1;
    if abs(n-697)<maxOffset
        f = 697;
    elseif abs(n-770)<maxOffset
        f = 770;
    elseif abs(n-852)<maxOffset
        f = 852;
    elseif abs(n-941)<maxOffset
        f = 941;
    elseif abs(n-1209)<maxOffset
        f = 1209;
    elseif abs(n-1336)<maxOffset
        f=1336;
    elseif abs(n-1477)<maxOffset
        f=1477;
    end
end