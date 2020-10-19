clear all;
close all;
clc;

IN_ONE_GRAF = 1;

[sig,Fs]=audioread('DOBRYDEN.WAV');
NBITS = 16;
disp('P�vodn� sign�l');
sound(sig,Fs);
if IN_ONE_GRAF == 1
    subplot(5,1,1)
else
    
end
plot(sig);
title('P�vodn� sign�l')
if IN_ONE_GRAF == 1
    
else
    pause
end

NBITSnew=7; %snizeni rozliseni z NBITS=16 na 5 bitu

%1. prevedeni signalu z rozsahu -1:1 (Matlab) do rozsahu odpovidaj�mu NBITS
signew = sig .* 2^(NBITS-1);
if IN_ONE_GRAF == 1
    subplot(5,1,2)
else
    pause
    close all;
end


plot(signew);
title('8 bit�')
disp('NBITS - 1 proto�e hodnoty jsou od -1 do 1')
if IN_ONE_GRAF == 1
    
else
    pause
end
%2. snizeni rozsahu dle NBITSnew = vydeleni vsech hodnot pomerem
%2^NBITS/2^NBITSnew => vydeleni 2^(NBITS-NBITSnew)
signew = signew ./ 2^(NBITS-NBITSnew);

if IN_ONE_GRAF == 1
    subplot(5,1,3)
else
    pause
    close all;
end

plot(signew);
title('Rozd�l')
if IN_ONE_GRAF == 1
    
else
    pause
end


signew = round(signew);
if IN_ONE_GRAF == 1
    subplot(5,1,4)
else
    close all;
end

plot(signew);
title('P�ekvantov�n�')
if IN_ONE_GRAF == 1
    
else
    pause
end


%3. normalizace do urovne -1:1 kvuli prehrani
signew = signew ./ 2^(NBITSnew-1);
if IN_ONE_GRAF == 1
    subplot(5,1,5)
else
    close all;
end

plot(signew);
title('Normalizace')
sound(signew,Fs);