clc;close all;clear all;
load('spoken_tren.mat');
use_nez = 1;
N = 0;
if(use_nez == 1)
    load('spoken_test_nez.mat');
    test_data = test_nez_data;
    test_trida = test_nez_trida;
    N = 1200;
else
    load('spoken_test.mat');
    N = 1000;
end
M = 4000;
dist = zeros(1,M);
tridy_vzoru = zeros(1,M);
pocet_pokusu=0; 
pocet_spravnych= 0;
tic;
start_time = toc;
profile on
disp(['Pripravuji obrazky'])
for j = 1:M
    v = tren_data(j,:,:);      
    tren_data(j,:,:) = preproc(v);
end
for  i = 1:N                     % cyklus pro test. obrázky, N max.1000
    %fprintf("%d/%d",i,N)
    toc
    x = test_data(i,:,:);        % načti test. obrázek
    x = preproc(x);
    x_trida= test_trida(i);      % jeho třída
    for j = 1:M                   % cyklus pro tren. vzory, M max.9000        
        v = tren_data(j,:,:);      % načti tren. obrázek
        v_trida= tren_trida(j);    % jeho třída 
        tridy_vzoru(j) = v_trida;   % ulož číslo třídy do pole tridy_vzoru
        dist(j)=sum((x-v).^2,'all'); % urči vzdálenost a ulož do pole dist
    end
    [min_dist, index] = min(dist); % nejmenší vzdálenost a její index
    nejblizsi_trida = tridy_vzoru(index); % třída nejbližšího vzoru
    if x_trida== nejblizsi_trida% je shoda?
        pocet_spravnych= pocet_spravnych + 1;  % pokud ano, započítej
    end
    pocet_pokusu=  pocet_pokusu + 1;         % započítej pokus
end
end_time = toc;
uspesnost = pocet_spravnych/pocet_pokusu* 100;% urči úspěšnost
disp(['Úspěšnost: ', num2str(uspesnost), '%']);
disp(['Čas: ',num2str(end_time-start_time) ]);
profile viewer

function y = preproc(x)
    ye = evenSpread(x);
    y = filter2d(ye,[1 1 1; 1 1 1; 1 1 1]);
    y = filter2d(y,[-1 -1 -1; -1 20 -1; -1 -1 -1]);
    y = boostContrast(y,ye);
    y = double(y)./max(y(:));
end


function y = boostContrast(x,org)
    y=x;
    bl = min(y(:)) + 80;
    for i=1:size(x,1)
        for j=1:size(x,2)
            if(y(i,j) < bl)
                y(i,j) = org(i,j);
            end
        end
    end
end

function y = evenSpread(x)
    d = 1./max(x(:)).*255;
    y =x.*d;
end

function dp = filter2d(x,f)
    dp = x;
    for n=1:size(x,2)
        for m=1:size(x,3)
            part = x(1,max(1,n-1):min(n+1,end),max(1,m-1):min(end,m+1));
            partf = f(1:size(part,1),1:size(part,2));
            part = part.*partf;
            dp(1,n,m) = sum(part(:))./sum(partf(:));
        end
    end
end