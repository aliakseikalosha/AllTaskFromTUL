function x_out = data_preprocessing_fast(x_in,x_tren_in)
    % x_in...vstupní testovaci data data, rozmìr: 1000x32x32 = Ntest x 32 x 32
    % x_tren_in...vstupní trenovací data, rozmìr: 9000x32x32 = Ntren x 32 x 32
    % x_out...výstupní testovací data

     % ========================== VÁŠ KÓD ZDE ======================
    % Nejprve støední hodnota jednotlivých pixelù trénovacích obrázkù pomocí pøíkazu mean:
    norm_factor = mean(x_tren_in,1);    

    %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    % Provedení normalizace bez cyklu %
    %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

    % Normalizaci x_in odeètením vypoètených 32 støedních hodnot lze zapsat i bez použití cyklu for jako maticovou operaci pomocí pøíkazu
    % REPMAT s parametrem N:
    N = size(x_in,1);
    % ========================== VÁŠ KÓD ZDE ======================
    x_out = x_in - repmat(norm_factor,[N 1 1]); 
    % Kontrola: x_out(10,10,10) = 4.83
    % =============================================================    
    
    %%%%%%%%%%%%%%%%%%%%%%%%%%%%
    % RESHAPE matice efektivnì %
    %%%%%%%%%%%%%%%%%%%%%%%%%%%%
    
    % Matici x_out o rozmìrech Nx32x32 dále pøevedeme na matici o rozmìrech Nx1024 pøíkazem RESHAPE.
    % Ten v Matlabu ovšem funguje po sloupcích, kdežto v trénovacím software obdoba pøíkazu reshape
    % zpracovávala bìhem trénování matice po øádcích, jako cykly for v pøedchozí implementaci => pøed použitím pøíkazu RESHAPE je tøeba prohodit
    % sloupce a øádky obrázkù pøíkazem PERMUTE:
    % ========================== VÁŠ KÓD ZDE ======================
    x_out = permute(x_out,[1 3 2]);     
    % Nyní je možné použít RESHAPE a výsledek bude stejný, jako v trénovacím
    % software (numpy+python) a jako byl pøi použití vnoøených cyklù:    
    x_out = reshape(x_out,[N size(x_out,2)*size(x_out,3)]);   
    % Kontrola: x_out(666,666) = 0.3272
    % =============================================================  
    % Pokud bychom PERMUTE neprovedli, nauèené váhy by se aplikovaly na
    % pøehozené vstupní hodnoty a pøesnost rozpoznávání by byla 12.4%, tedy pøibližnì 1/C = 1/10!!!
    
    % Zbývá jen doplnit každý vektor testovacích dat o hodnotu 1, aby šlo násobení s vektorem vah    
    % provádìt jako skalární souèin:
    x_out = [x_out ones(N,1)];
    % x_out má nyní rozmìr N*1025 !!!!
       
   
end
