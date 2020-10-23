clc;clear all;close all;
fileID = fopen('MojeReseniPohlavi.txt','r');
textdata = textscan(fileID,'%s');  
fclose(fileID);
guess = string(textdata{:});
fileID = fopen('SpravneReseniPohlavi.txt','r');
textdata = textscan(fileID,'%s');  
fclose(fileID);
answer = string(textdata{:});
n = size (guess, 1);
success = 0;
fileID = fopen('Protokol_Kalosha.txt','a');
for i = 1:n   
    if answer(i) == guess(i)
        success = success+1;
    end
    fprintf(fileID,"%s %s\n",answer(i),guess(i));
    fprintf("%s %s\n",answer(i),guess(i));
end
fprintf(fileID,"gender success rate : %.3f%%\n",success/n*100);
fprintf("gender success rate : %.3f%%\n",success/n*100);
fclose(fileID);