function z = functionCv5(x,y)
if nargin < 2 
   y = zeros(1, length(x)); 
end
z = x.*exp(-x.^2 - y.^2)+ tanh(x.*y);
end

