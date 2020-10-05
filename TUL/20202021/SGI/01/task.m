clc
clear global
close all
disp('Uloha 1')
[s,Fn] = audioread('hudba_praskani.wav');
sN = s;
plot(sN)
hold on
maxN = max(s);
minN = min(s);
for i = 1:size(s)
   if (s(i) == maxN) | (s(i) == minN)
        if (i == 1) | (i == size(s))
            disp(i);
            s(i) = 0;
        else
            disp(i);
            s(i) = (s(i-1)+s(i+1))/2;
        end
   end
end
plot(s)
sound(s)
legend('s praskanim','bez praskani');
audiowrite('hudba_bez_praskani.wav',s,Fn);

disp('Odstranit mezeru?')
pause
iStart=-1;
iEnd=-1;
for i = 2:size(s)-1
    if (iStart ~= -1) & (s(i)~=0)
        iEnd = i;
    end
    if iEnd ~= -1
        middle = (iEnd-iStart)/2;
        block = s(iStart:iEnd);
        
        for j=2:size(block)-1
            if  j<=middle
                block(j) = block(j-1)-s(iStart)*2/middle;
            else
                block(j) = block(j-1)+(s(iEnd)- block(floor(middle)))/middle;
            end
        end
        
        s(iStart:iEnd) = block;
        iStart=-1;
        iEnd=-1;
    end
    if (iStart == -1) & (s(i-1) ~= 0) & (s(i) == 0) & (s(i+1) == 0)
        iStart = i-1;
   end
end
plot(s)
sound(s)
legend('s praskanim','bez praskani','bez mezery');
audiowrite('hudba_bez_praskani_bez_mezery.wav',s,Fn);
hold off