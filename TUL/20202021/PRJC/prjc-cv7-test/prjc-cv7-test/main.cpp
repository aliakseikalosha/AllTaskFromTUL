#include <malloc.h>
#include <stdio.h>
#define _CRT_SECURE_NO_WARNINGS
/* TASK 01
int* createArray(int size) {
	return (int*)malloc(sizeof(int) * size);
}

int** sort(int* a, int size) {
	int** r = (int**)malloc(sizeof(int*) * 2);
	if (r == NULL) {
		return NULL;
	}
	r[0] = (int*)malloc(sizeof(int) * (size + 1));
	if (r[0] == NULL) {
		free(r);
		return NULL;
	}
	r[1] = (int*)malloc(sizeof(int) * (size + 1));
	if (r[1] == NULL) {
		free(r);
		free(r[0]);
		return NULL;
	}
	r[0][0] = 0;
	r[1][0] = 0;
	int index = 0;
	for (size_t i = 0; i < size; i++)
	{
		index = a[i] % 2;
		r[index][0]++;
		r[index][r[index][0] + 1] = a[i];
	}
	return NULL;
}
*/
/* TASK 02
typedef struct graph;
typedef struct node;
typedef struct conn;

typedef struct graph {
	node* nodes;
	int number_of_nodes;
}Graph;

typedef struct node {
	conn* conections;
	int number_of_conections;
}Node;

typedef struct conn {
	Node* from;
	Node* to;
}Connection;

int addNode(Graph* g) {
	void* old_p = (void*)g->nodes;
	g->nodes = (node*)realloc(g->nodes, sizeof(node) * (g->number_of_nodes + 1));
	if (g->nodes == NULL) {
		g->nodes = (node*)old_p;
		return -1;
	}
	else {
		node* n = &g->nodes[g->number_of_nodes - 1];
		n->number_of_conections = g->number_of_nodes;
		n->conections = (conn*)malloc(sizeof(conn) * n->number_of_conections);
		if (n->conections == NULL) {
			//revert changes
			return -1;
		}
		conn* c;
		for (size_t i = 0; i < g->number_of_nodes-1; i++)
		{
			c = &n->conections[i];
			c->from = n;
			c->to = &g->nodes[i];
		}
	}
	return 0;
}
*/
/* TASK 03
float** create(int size) {
	float** t = (float**)malloc(sizeof(float*) * size);
	if (t != NULL) {
		for (int i = 0; i < size; i++)
		{
			t[i] = (float*)malloc(sizeof(float) * (i * 2 + 1));
			if (t[i] == NULL) {
				//free all previos allocation
				return NULL;
			}
		}
	}
	return t;
}

void fill(float** t, int size) {
	for (int i = 0; i < size; i++)
	{
		for (int j = 0; j < i*2+1; j++)
		{
			t[i][j] = (((float)i)+1)/(j+1);
		}
	}
}

int main() {
	int size = 5;
	float** t = create(size);
	fill(t, size);
	for (int i = 0; i < size; i++)
	{
		for (int j = 0; j < i * 2 + 1; j++)
		{
			printf("%f\t", t[i][j]);
		}
		printf("\n");
	}
	getchar();
}
*/

char** create(int size) {
	size = size > 9 ? 9 : size;
	char** t = (char**)malloc(sizeof(char*) * size);
	if (t == NULL) {
		return NULL;
	}
	for (int i = 0; i < size; i++)
	{
		t[i] = (char*)malloc(sizeof(char) * (i + 1));
		if (t[i] == NULL) {
			for (int j = 0; j < i; j++)
			{
				free(t[j]);
			}
			return NULL;
		}
		for (int j = 0; j <= i; j++)
		{
			t[i][j] = 65 + j;
		}
	}
	return t;
}

int test(char** t, int size) {
	size = size > 9 ? 9 : size;
	/*nevim je-li vhodne pouzivat _msize() abych zistil velikost pole char** t tim bych mohl overzit omezemi vysky na 9
	* za mne lepsim resenim by bylo vratit v metode create struct {char** t;int vyshka}
	* takze prozatim tady jenom overzeni pismen
	*/
	if (t == NULL)
	{
		return -1;
	}
	for (int i = 0; i < size; i++)
	{
		if (t[i] == NULL)
		{
			return -1;
		}
		for (int j = 0; j <= i; j++)
		{
			if (t[i][j] != 65 + j) {
				return -1;
			}
		}
	}
	return 0;
}

int main() {
	int size = 22;
	printf("test : %d",test(create(size),size));
}
