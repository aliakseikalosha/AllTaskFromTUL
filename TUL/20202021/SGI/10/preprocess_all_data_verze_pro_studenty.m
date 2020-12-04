clear all;

% naètení fileListu
fileID = fopen('FileList.txt','r'); %otevreni seznamu, ktery si musite vytvorit
textdata = textscan(fileID,'%s');  %nacten
fclose(fileID); % zavreni seznamu
soubory = string(textdata{:}); %pole cest
pocet_nahravek = size (soubory, 1); %pocet nahravek

% pozadovane parametry 
pocet_vzorku_v_segmentu = 256;
pocet_segmentu_ve_vyrezu =  % doplnte podle zadani
pocet_priznaku_na_segment = % doplnte podle zadani

% prealokace nekterych poli (kvuli zvyseni rychlosti) 
% - vyznam promennych je objasnen na prislusnem miste
energie = zeros(32000/256,1);
prevzorkovane_amp_spektrum = zeros(pocet_priznaku_na_segment,1);
priznaky_pro_rozpoznavani= zeros (pocet_segmentu_ve_vyrezu, pocet_priznaku_na_segment);
spgram = zeros (pocet_segmentu_ve_vyrezu, pocet_priznaku_na_segment);
all_data = zeros (pocet_nahravek,pocet_segmentu_ve_vyrezu, pocet_priznaku_na_segment);
all_trida =  zeros(pocet_nahravek,1); all_person =  zeros(pocet_nahravek,1); all_sada =  zeros(pocet_nahravek,1);


for n = 1:pocet_nahravek 
    soubory(n, :)      % vypise jmeno zpracovavane nahravky
    [sig, Fs] = audioread(soubory(n, :));  % nacte nahravku
    
    figure('Name',soubory(n, :));   % vypise jmeno nahravky do zahlavi obrazku
    subplot(2,2,1);    
    plot(sig);   % zobrazi cely signal
    title('Originalni signal')
   
    % segmentace signalu a vypocet energie v segmentech
    pocet_segmentu  = length (sig) /pocet_vzorku_v_segmentu;
    for i = 1:pocet_segmentu
        segment = sig((i-1)*pocet_vzorku_v_segmentu+1:i*pocet_vzorku_v_segmentu);  % vyriznuti segmentu
        energie(i) = dot(segment,segment);     % vypocet energie v i-tem segmentu
        % zjistete si vyznam funkce dot ktera je velmi efektivni, 
        % to ze ji rozumite dokazte tim, ze ji nahradite jinym asi mene efetivnim pristupem
    end
    
    subplot(2,2,2);
    plot(energie);   % vykresleni prubehu energie
    title('Energie orig. signalu')
    
    % hledani teziste prubehu energie
    tot_energie = sum(energie);   % celkova energie pres vsechny segmenty
    i_vector = 1:pocet_segmentu;  % vektor indexu segmentu 
    teziste = round(dot(energie, i_vector')/tot_energie); % index segmentu kde lezi teziste 
    hold on;
    plot(teziste, energie(teziste),'r*');   % zakresli do grafu pozici teziste
    hold off;

    % vyrez signalu (o delce 64 segmentu) symetricky kolem teziste
    prvni_segment = teziste-pocet_segmentu_ve_vyrezu/2;  % v tomto segmentu zacina vyrez
    posledni_segment = prvni_segment + pocet_segmentu_ve_vyrezu - 1; 
    vyrez_orig = sig((prvni_segment-1)*pocet_vzorku_v_segmentu+1:posledni_segment*pocet_vzorku_v_segmentu);
    
    % protoze vyssi frekvence hraji pro rozpoznavani reci vetsi roli, aplikujeme na vyriznuty signal 
    % jednoduchou horni propust, v tomto pripade staci derivator (tez diferenciator) - viz napr. prednaska 9 
    % v nasledujici radku doplnte funkci filter tak, aby ze signalu
    % 'vyrez_orig' udelala filtrovany signal 'vyrez'
    vyrez = filter (......... vyrez_orig);
  
    % zobrazeni a prehrani vyrezu signalu
    subplot(2,2,3); 
    plot(vyrez);
    title('Vyriznuty signal')
    sound (vyrez, Fs); 
    
    % vypocet spektra v jednotlivych segmentech vyrezu
    for i = 1:pocet_segmentu_ve_vyrezu
       segment = vyrez((i-1)*pocet_vzorku_v_segmentu+1:i*pocet_vzorku_v_segmentu);  % segment obsahuje 256 vzorku
       segment = segment .* hamming (pocet_vzorku_v_segmentu)';   % vynasobime hammingovou vahou
       spektrum = fft(segment);   % spektrum jednoho segmentu spocitane pomoci fft
       amp_spektrum = abs (spektrum(1:pocet_vzorku_v_segmentu/2)); % amplitudove spektrum obsahujici 128 hodnot
       % ze 128 hodnot chceme ziskat pouze 64 hodnot, proto ze dvou sousednich hodnot vypocitame prumer
       for j = 1:pocet_priznaku_na_segment
           prevzorkovane_amp_spektrum(j)= (amp_spektrum(2*j-1)+amp_spektrum(2*j))/2;  % umite tento prepocet udelat efektivneji?
       end
       priznaky_pro_rozpoznavani(i, :) = prevzorkovane_amp_spektrum;  % ulozime jako spektrum segmentu do matice
    end
    
    %zde provedte vypis matice priznaku do textoveho souboru (nejlepe pomoci printf, 
    % abyste si mohli priznaky prohlednout {omezte pocet platnych cifer na nutne minimum)
    % .....
    
    % zobrazeni matice priznaku - jedna se vlastne o hodnoty spektrogramu, takze ho
    % vyobrazime, jak jsme zvykli, tentokrat pouze ve stupnici sede 
    for i = 1:pocet_segmentu_ve_vyrezu
        spgram(pocet_priznaku_na_segment:-1:1, i) = priznaky_pro_rozpoznavani (i, :);  % spektrum i-teho segmentu 
              % vysvetlete zvlastni manipulaci s indexy u pole spgram ........
              
    end
    I = mat2gray(spgram,[0.1 0]);  % zobrazime hodnoty matice pomoci sedotonove stupnice
                                   % proc asi jsem zvolil hodnotu mezi 0.1 a 0? co se stane kdyz to bude 1 0?
    
    subplot(2,2,4);
    imshow(I);
    title('Matice priznaku zobrazena jako spektrogram')
    
    % data pro jednu nahravku jsou spocitana, pripravime je k ulozeni
    % z nazvu souboru odvodime identifikatory recene cislice, osoby a sady
    k = strfind (soubory(n, :), ".wav");   % zjistime pozici, kde konci nazev souboru
    trida = double(str2num(soubory(n, k-11))); % identifikator cislice nam bude slouzit pro rozpoznavani
    person = double(str2num(soubory(n, k-8:k-5)));  % identifikator osoby
    sada = double(str2num(soubory (n,k-2:k-1)));    % identifikator sady
  
    all_data(n,:,:) = priznaky_pro_rozpoznavani;   % v tomto poli budou ulozena vsechna data, tj. vzdy matice 64x64 pro kazdou nahravku
    all_trida(n) =  trida; % do toho vektoru ulozime informaci, o jakou cislici se jedna (z hlediska rozpoznavani se to nazyva trida) 
    all_person(n) = person;  % ulozeni informace o osobe
    all_sada(n)  = sada;   % ulozeni informace o sade
  
    pause   % vhodne v rezimu prohlizeni obrazku

    close   % zavreni obrazku - umite to jinak a lepe?
  
end

% zaverecne ulozeni vsech dat do jedineho souboru
save ('spoken_digits_ukazka.mat', 'all_data', 'all_trida', 'all_person', 'all_sada')
 
