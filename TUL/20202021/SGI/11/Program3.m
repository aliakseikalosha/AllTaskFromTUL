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
DP = [1 1 1; 1 1 1; 1 1 1];
HP = [-1 -1 -1; -1 20 -1; -1 -1 -1];
start_time = toc;
%profile on 
for j = 1:M
    tren_data(j,:,:) = preproc(tren_data(j,:,:),HP,DP);
end
x = zeros(1,64,64);
for  i = 1:N                     % cyklus pro test. obrázky, N max.1000
    x(1,:,:) = preproc(test_data(i,:,:),HP,DP);
    for j = 1:M                   % cyklus pro tren. vzory, M max.9000        
        dist(j)=sum((x-tren_data(j,:,:)).^2,'all'); % urči vzdálenost a ulož do pole dist
    end
    [min_dist, index] = min(dist); % nejmenší vzdálenost a její index
    nejblizsi_trida = tren_trida(index); % třída nejbližšího vzoru
    if test_trida(i) == nejblizsi_trida% je shoda?
        pocet_spravnych= pocet_spravnych + 1;  % pokud ano, započítej
    end
    pocet_pokusu=  pocet_pokusu + 1;         % započítej pokus
end
end_time = toc;
uspesnost = pocet_spravnych/pocet_pokusu* 100;% urči úspěšnost
disp(['Úspěšnost: ', num2str(uspesnost), '%']);
disp(['Čas: ',num2str(end_time-start_time) ]);
%profile viewer

function y = preproc(x,h,d)
    ye = evenSpread(x);
    y = filter2d(ye,h);
    y = filter2d(y,d);
    y = double(y)./max(y,[],'all');
end

function y = evenSpread(x)
    y = x./max(x,[],'all').*255;
end

function dp = filter2d(x,f)
    dp = x;
    for n=1:64
        nmax = max(1,n-1);
        nmin = min(n+1,64);
        for m=1:64
            part = x(1,nmax:nmin,max(1,m-1):min(64,m+1));
            partf = f(1:size(part,1),1:size(part,2));
            part = part.*partf;
            dp(1,n,m) = sum(part,'all')/sum(partf,'all');
        end
    end
end