import { updateStatus } from "../services/workRequestService";

function WorkRequestList({ requests, onRefresh }) {
  const handleStatusChange = async (id, status) => {
    await updateStatus(id, status);
    onRefresh();
  };

  return (
    <div className="requests-grid">
      {requests.map((item) => (
        <div key={item.id} className="card">
          <h3>{item.title}</h3>

          <div className="meta">
            Client: {item.clientName}
          </div>

          <div className="status-badge">
            {item.status}
          </div>

          <p>{item.description}</p>

          <div className="meta">
            Priority: {item.priority}
          </div>

          <div className="meta">
            Due: {new Date(item.dueDate).toLocaleDateString()}
          </div>

          <br />

          <select
            defaultValue={item.status}
            onChange={(e) =>
              handleStatusChange(item.id, e.target.value)
            }
          >
            <option value="New">New</option>
            <option value="InProgress">In Progress</option>
            <option value="Blocked">Blocked</option>
            <option value="Completed">Completed</option>
          </select>
        </div>
      ))}
    </div>
  );
}

export default WorkRequestList;
