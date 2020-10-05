function [y] = polynomTest(x,a)
y = 0;
for i=1:length(a)
    y = y+a(i)*x.^(i-1);
end
end

