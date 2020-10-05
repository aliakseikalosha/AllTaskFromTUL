clear;
clc;
%1
C = randn(ceil(10+30*rand),floor(10+30*rand), fix(10+30*rand));
%2
%for
p = 0; 
for x = 1:size(C(:))
    p = p + C(x);
end
p = p/size(C(:),1)
%sum
p = sum(C(:))/size(C(:),1)
%3
C1 = squeeze(C(1,:,:));

C1 = C1 - (sum(C1')/size(C1,2))';