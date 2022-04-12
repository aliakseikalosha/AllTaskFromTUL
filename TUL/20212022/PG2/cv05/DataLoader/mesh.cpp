#include "mesh.h"
#include "texture.h"

void mesh::init(GLenum primitive_type, std::vector<vertex>& vertices, std::vector<GLuint>& indices, cv::Mat& teximage)
{
	this->primitive_type = primitive_type;
	this->vertices = vertices;
	this->indices = indices;
	this->tex_ID = tex_gen(teximage);

	init();
}

//-----------------------------------------------------------------------------------------------------
// Friend function

void mesh_draw(mesh& mesh)
{
    /*
	glBindTexture(GL_TEXTURE_2D, mesh.tex_ID);
	glBindVertexArray(mesh.VAO_ID);
	glDrawElements(mesh.primitive_type, mesh.indices.size(), GL_UNSIGNED_INT, 0);
	glBindVertexArray(0);
     */
}
