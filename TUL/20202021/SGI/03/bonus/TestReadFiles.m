% ukazkovy program, jak nacist nazvy testovacich souboru ze seznamu 
% a jak s nimi pak pracovat.
fileID = fopen('learnFiles.txt','r');  % seznam otevren pro cteni
textdata = textscan(fileID,'%s')  % nacten to textdata
fclose(fileID); % seznam zavren 
fileNames = string(textdata{:}) % zde je vytvoreno stringove pole, obsahujici nazvy souboru
numFiles = size (fileNames, 1);
for i = 1:numFiles   % zde uz jen nacitam zvuky z jednotlivych souboru
    [x,Fs] = audioread(fileNames(i));
    plot (x);        % a delam s nimi, co ptrebuji
    sound (x, Fs);
    pause(1);
end

