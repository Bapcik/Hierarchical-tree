// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", async function () {
    const response = await fetch("/Home/GetRoot");
    const data = await response.json();
    displayData(data, document.getElementById("rootElement"));
});

function displayData(items, container) {
    items.forEach((item) => {
        const li = document.createElement("li");
        li.dataset.id = item.id;
        li.style = "list-style:none; margin-bottom:4px;";

        li.innerHTML = `
            <div class="d-flex align-items-center gap-2 p-2 border rounded">
                ${item.hasChildren
                ? `<button class="btn btn-sm btn-outline-secondary toggle-btn"></button>`
                : `<span style="width:28px; display:inline-block;"></span>`
            }
                <span class="flex-grow-1">${item.name}</span>


            <div class="dropdown">
                <button class="btn btn-sm btn-outline-secondary dropdown-toggle" data-bs-toggle="dropdown">
                </button>
                <ul class="dropdown-menu dropdown-menu-end">
                    <li>
                        <button type="button" class="dropdown-item create-btn" data-bs-toggle="modal" 
                        data-bs-target="#createModal" data-id="${item.id}">
                            Добавить
                        </button>
                    </li>

                   ${item.parentId !== null
                ? `
                        <li>
                            <button type="button" class="dropdown-item edit-btn" data-bs-toggle="modal" 
                            data-bs-target="#editModal" data-id="${item.id}" data-name="${item.name}" data-parent-id="${item.parentId}">
                                Редактировать
                            </button>
                        </li>

                        <li>
                            <button type="button" class="dropdown-item text-danger delete-btn" data-bs-toggle="modal"
                                    data-bs-target="#deleteModal" data-id="${item.id}" data-name="${item.name}">
                                Удалить
                            </button>
                        </li>
                  `
                : ""
            }
                </ul>
            </div>
                
            </div>
            ${item.hasChildren
                ? `<ul class="dd-list" id="${item.id}" data-loaded="false" style="display:none;"></ul>`
                : ""
            }
        `;

        if (item.hasChildren) {
            li.querySelector(".toggle-btn").addEventListener("click", () =>
                toggleChildren(item.id),
            );
        }

        container.appendChild(li);
    });
}

async function toggleChildren(id) {
    const childrenList = document.getElementById(id);

    if (childrenList.dataset.loaded === "false") {
        const response = await fetch(`/Home/GetChildren?id=${id}`);
        const children = await response.json();
        displayData(children, childrenList);
        childrenList.dataset.loaded = "true";
    }

    childrenList.style.display =
        childrenList.style.display === "none" ? "block" : "none";
}

document.addEventListener("click", (e) => {
    if (e.target.classList.contains("create-btn")) {
        const { id } = e.target.dataset;

        document.getElementById("createParentId").value = id;
        document.getElementById("createForm").action = `/Home/Create`;
    }
});

document.addEventListener("click", (e) => {
    if (e.target.classList.contains("edit-btn")) {
        const { id, name, parentId } = e.target.dataset;

        document.getElementById("editName").value = name;
        document.getElementById("editParentId").value = parentId;
        document.getElementById("editForm").action = `/Home/Edit/${id}`;
    }
});

document.addEventListener("click", (e) => {
    if (e.target.classList.contains("delete-btn")) {
        const { id, name } = e.target.dataset;

        document.getElementById("deleteName").textContent = name;
        document.getElementById("deleteForm").action = `/Home/Delete/${id}`;
    }
});
