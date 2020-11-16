clear all;clc;close all
xls_table = readtable('Covid20201110.xls');
dates = table2array(xls_table(:,1));
cases = table2array(xls_table(:,2));
s = size (cases,1);

fileID = fopen('Protokol_Kalosha.txt','a');
% vyhlazeni dat tydennim klouzavym prumerem
cases_smoothed = zeros (s,1);
for d = 4:s-4
    cases_smoothed(d)= floor(sum(cases(d-3:d+3))/7);
end

plot (cases,'g')
hold on
plot (cases_smoothed,'r')
xline(219) % 1. zari
xline(278) % 30. rijen

x = cases_smoothed; % zkopirujeme do vektoru x

% definovani poli pro vysledky
pred = zeros (7,1);   % predikce pro dny D, D+1, ... D+6
abs_error = zeros (7,1);  % absol. hodnoty odchylek
sum_errors = zeros (7,1);  % soucty odchylek

fig = figure('Renderer', 'painters', 'Position', [10 10 1024 768]);
% nyni cyklus vypoctu pro dny od 1.9. do 30.10.
for d = 219:278
    % minulost tvori 7 dni pred dnem D 
    % a z jejich hodnot chceme predikovat pocty pripadu pro dny D, D+2 a D+6
    
    % zde bude vas vypocet predicniho modelu
    %-----------------------------------------
    % ja zde jako priklad uvadim velmi trivialni model kdy kazdy den pribyde
    % o 1 pripad vice
        fprintf(fileID,"%s\n",dates(d));
        fprintf("%s\n",dates(d))
        g = guessUltimate((d-7:d-1),x(d-7:d-1));
        
        hold off
        plot((d-7:d-1),x(d-7:d-1),'r')
        hold on
        plot((d:d+6),x(d:d+6),'b')
        plot(d:d+6,g(1,:),'b--')
        plot(d:d+6,g(2,:),'k--')
        plot(d:d+6,g(3,:),'c--')
        plot(d:d+6,g(4,:),'g--')
        plot(d:d+6,sum(g,1)./size(g,1),'m-o')
        g = guessBest((d-7:d-1),x(d-7:d-1));
        plot(d:d+6,g,'g-*')
        legend({'past 7 day','future 7 day','prediction exp','prediction poly1 last 2 day','prediction poly2 last 7','prediction poly2 days last 2-6', 'best result prediction'},'Location','best')
        saveas(fig, sprintf("preditiction_for_%d.png",d));
        
        % trivialni predikce pro den d
        pred(1) = floor (g(1)); % floor proto, ze chceme aby odhad byl vzdy celociselny
        abs_error(1) = abs(pred(1) - x(d));
        fprintf (fileID,'den d   - real: %d  pred: %d  abs_error: %d\n',x(d), pred(1), abs_error(1));
        fprintf ('den d   - real: %d  pred: %d  abs_error: %d\n',x(d), pred(1), abs_error(1))

        % trivialni predikce pro den d+2
        pred(3) = floor (g(3));
        abs_error(3) = abs(pred(3) - x(d+2));
        fprintf (fileID,'den d+2 - real: %d  pred: %d  abs_error: %d\n',x(d+2), pred(3), abs_error(3));
        fprintf ('den d+2 - real: %d  pred: %d  abs_error: %d\n',x(d+2), pred(3), abs_error(3));

        % trivialni predikce pro den d+6
        pred(7) = floor (g(7));
        abs_error(7) = abs(pred(7) - x(d+6));
        fprintf (fileID,'den d+6 - real: %d  pred: %d  abs_error: %d\n\n',x(d+6), pred(7), abs_error(7));
        fprintf ('den d+6 - real: %d  pred: %d  abs_error: %d\n\n',x(d+6), pred(7), abs_error(7));
   
    sum_errors = sum_errors + abs_error; % nascitani chyb za kazdy den
    %-----------------------------------------
    pause(0.2)
end

fprintf (fileID,'\nSoucet chyb \nden d: %d  \nden d+2: %d   \nden d+6: %d\n\n',sum_errors(1), sum_errors(3), sum_errors(7));
fprintf ('\nSoucet chyb \nden d: %d  \nden d+2: %d   \nden d+6: %d\n\n',sum_errors(1), sum_errors(3), sum_errors(7))
fclose(fileID);



function g = guessBest(x,y)
    gp1 = zeros(7,7);
    g = 1:7;
    for i = 1:7
        gp1(i,:) = guessPoly1(x,y,i);
    end
%     g(1) = sum(gp1(:,1)'.*(7:-1:1))./sum(7:-1:1);
%     g(2) = sum(gp1(:,2)'.*[6,7,5,4,3,2,1])./sum([6,7,5,4,3,2,1]);
%     g(3) = sum(gp1(:,3)'.*([5,6,7,4,3,2,1]))./sum([5,6,7,4,3,2,1]);
%     g(4) = sum(gp1(:,4)'.*([4,5,6,7,3,2,1]))./sum([4,5,6,7,3,2,1]);
%     g(5) = sum(gp1(:,5)'.*([3,4,5,6,7,2,1]))./sum([5,6,7,4,3,2,1]);
%     g(6) = sum(gp1(:,6)'.*([2,3,4,5,6,7,1]))./sum([5,6,7,4,3,2,1]);
%     g(7) = sum(gp1(:,7)'.*(1:7))./sum(1:7);
    g = (gp1(1,:)+gp1(2,:))./2; 
end

function g = guessUltimate(x,y)
   
    f={};
    f{1} = fit(x',y,'exp1');
    n=1;
    f{2}= fit(x(end-min(n,length(x)-1):end)',y(end-min(n,length(x)-1):end),'poly1');
    n=6;
    f{3} = fit(x(end-n:end)',y(end-n:end),'poly2');
    f{4} = fit(x(end-6:end-2)',y(end-6:end-2),'poly2');
    err = [];
    g = zeros(length(f),7);
    for i =1:length(f)
        fi=f{i};
        err(i) = sum(abs(y-fi(x)));
        g(i,:)=fi(x(end)+1:x(end)+7);
    end
%     ff=f{find(err==min(err))};
%     g = ff(x(end)+1:x(end)+7);
end

function g = guessPoly1(x,y,n)
    f = fit(x(end-min(n,length(x)-1):end)',y(end-min(n,length(x)-1):end),'poly1');
    g = f(x(end)+1:x(end)+7);
end

function g = guessPoly2(x,y,n)
    n = max(min(2,n),length(x)-1);
    f = fit(x(end-n:end)',y(end-n:end),'poly2');
    g = f(x(end)+1:x(end)+7);
end

function g = guessExp1(x,y)
    f = fit(x',y,'exp1');
    g = f(x(end)+1:x(end)+7);
end

function g = guessR(x,y)
    r = zeros(1,length(y)-1);
    for i = 1:length(r)
        r(i) = y(i+1)/y(i);
    end
    fr = fit(x(end-2:end)',r(end-2:end)','poly1');
    for i = 1:7
        y(end+1) = (y(end))*fr(x(end)+i);
    end
    g = y(end-7:end-1);
end
