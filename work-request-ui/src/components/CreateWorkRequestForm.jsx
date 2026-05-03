import { useState } from "react";
import { createWorkRequest } from "../services/workRequestService";

function CreateWorkRequestForm({ onCreated }) {
  const [form, setForm] = useState({
    title: "",
    clientName: "",
    description: "",
    priority: "Medium",
    status: "New",
    dueDate: ""
  });

  const [errors, setErrors] = useState({});

  const validate = () => {
    let newErrors = {};

    if (!form.title.trim()) newErrors.title = "Title is required";
    if (!form.clientName.trim()) newErrors.clientName = "Client name is required";
    if (!form.description.trim()) newErrors.description = "Description is required";
    if (!form.dueDate) newErrors.dueDate = "Due date is required";

    setErrors(newErrors);

    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!validate()) return;

    try {
      await createWorkRequest(form);

      setForm({
        title: "",
        clientName: "",
        description: "",
        priority: "Medium",
        status: "New",
        dueDate: ""
      });

      setErrors({});
      onCreated();
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div className="form-card">
      <h2>Create Work Request</h2>

      <form onSubmit={handleSubmit}>
        <div className="form-grid">
          <div>
            <input
              placeholder="Title"
              value={form.title}
              onChange={(e) =>
                setForm({ ...form, title: e.target.value })
              }
            />
            {errors.title && <p className="error">{errors.title}</p>}
          </div>

          <div>
            <input
              placeholder="Client Name"
              value={form.clientName}
              onChange={(e) =>
                setForm({ ...form, clientName: e.target.value })
              }
            />
            {errors.clientName && <p className="error">{errors.clientName}</p>}
          </div>

          <div>
            <select
              value={form.priority}
              onChange={(e) =>
                setForm({ ...form, priority: e.target.value })
              }
            >
              <option value="Low">Low</option>
              <option value="Medium">Medium</option>
              <option value="High">High</option>
            </select>
          </div>

          <div>
            <input
              type="date"
              value={form.dueDate}
              onChange={(e) =>
                setForm({ ...form, dueDate: e.target.value })
              }
            />
            {errors.dueDate && <p className="error">{errors.dueDate}</p>}
          </div>
        </div>

        <br />

        <div>
          <textarea
            placeholder="Description"
            value={form.description}
            onChange={(e) =>
              setForm({ ...form, description: e.target.value })
            }
          />
          {errors.description && (
            <p className="error">{errors.description}</p>
          )}
        </div>

        <br />

        <button type="submit">Create Request</button>
      </form>
    </div>
  );
}

export default CreateWorkRequestForm;