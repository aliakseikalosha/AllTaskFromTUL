%známe pravdìpodobnost odhadu volebního výsledku

%šíøka intervalu je pmax-pmin
sirka=0.02;
p=0:0.001:1;

rozsah=(4.*p.*(1-p)).*(norminv(0.975,0,1)).^2./(sirka.*sirka);
plot(p,rozsah)

max(rozsah)