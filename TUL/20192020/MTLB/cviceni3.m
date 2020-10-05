clear;
clc;
%1
A = randn(1000,2000);
%2
B = round(A);
%3

for i = 1:size(B,2)
   for j = 1:(size(B,2)-i)
        for x = 1:size(B) 
            if B(x,j) < B(x,j+1)
                c = B(:,j);
                B(:,j) = B(:,j+1);
                B(:,j+1) = c;
                break
            elseif B(x,j) > B(x,j+1)
                break
            end
        end
   end
end

