#pragma once
#include <GL/glew.h>
#include <vector>
#include <string>

#include <opencv2/core.hpp>
#include <glm/glm.hpp>
#include <GLFW/glfw3.h>

// vertex definition

struct vertex {
	glm::vec3 position;
	glm::vec2 tex_coord;
	glm::vec3 normal;
};

class mesh {
public:
	std::vector<vertex> vertices;
	std::vector<GLuint> indices;

	GLuint VAO_ID = 0;
	GLenum primitive_type = GL_POINTS;
	GLuint tex_ID = 0;

	mesh() = default;

	void init(void)
	{
		GLuint VBO_ID = 0, EBO_ID = 0;
        /*
        glGenVertexArrays(1, &VAO_ID);
		glBindVertexArray(VAO_ID);

		glGenBuffers(1, &VBO_ID);
		glBindBuffer(GL_ARRAY_BUFFER, VBO_ID);
		glBufferData(GL_ARRAY_BUFFER, vertices.size() * sizeof(vertex), vertices.data(), GL_STATIC_DRAW);

		glGenBuffers(1, &EBO_ID);
		glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO_ID);
		glBufferData(GL_ELEMENT_ARRAY_BUFFER, indices.size() * sizeof(GLuint), indices.data(), GL_STATIC_DRAW);

		glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(vertex), reinterpret_cast<void*>(0 + offsetof(vertex, position)));
		glEnableVertexAttribArray(0);

		glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, sizeof(vertex), reinterpret_cast<void*>(0 + offsetof(vertex, tex_coord)));
		glEnableVertexAttribArray(1);

		glBindVertexArray(0);
		glBindBuffer(GL_ARRAY_BUFFER, 0);
		glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
         */
	}

	void init(GLenum primitive_type, std::vector<vertex>& vertices, std::vector<GLuint>& indices)
	{
		this->primitive_type = primitive_type;
		this->vertices = vertices;
		this->indices = indices;

		init();
	}

	void init(GLenum primitive_type, std::vector<vertex>& vertices, std::vector<GLuint>& indices, cv::Mat& teximage);

	void clear(void) {
		vertices.clear();
		indices.clear();
		primitive_type = GL_POINTS;
		glDeleteVertexArrays(1, &VAO_ID);
	}
};

void mesh_draw(mesh& mesh);
