function x_out = data_preprocessing_cnn(x_in,x_tren_in)
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
    x_out = x_in - repmat(norm_factor,N,1);    
    % Kontrola: x_out(10,10,10) = 4.83
    % =============================================================                    
end
