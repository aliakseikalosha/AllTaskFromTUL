x=[987,1001,993,994,993,1005,1007,999,995,1002];

[h,p,ci,stats] = vartest(x,100)

%odhad smìrodatné odchylky. 
%odhad rozptylu je uložen v promìnné ci, takže provedeme po složkách jejich
%odmocninu a výsledek je odhad smìrodatné odchylky

std_ci=sqrt(ci)