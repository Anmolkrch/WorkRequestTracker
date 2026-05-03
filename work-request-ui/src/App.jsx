import { useEffect, useState } from "react";
import {
  getWorkRequests
} from "./services/workRequestService";
import WorkRequestList from "./components/WorkRequestList";
import CreateWorkRequestForm from "./components/CreateWorkRequestForm";

function App() {
  const [requests, setRequests] = useState([]);
  const [status, setStatus] = useState("");
  const [search, setSearch] = useState("");
  const [loading, setLoading] = useState(false);

  const loadRequests = async () => {
    try {
      setLoading(true);
      const data = await getWorkRequests(status, search);
      setRequests(data);
    } catch (error) {
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadRequests();
  }, [status, search]);

  return (
  <div className="container">
    <h1>Work Request Tracker</h1>

    <div className="toolbar">
      <input
        placeholder="Search by title or client"
        value={search}
        onChange={(e) => setSearch(e.target.value)}
      />

      <select
        value={status}
        onChange={(e) => setStatus(e.target.value)}
      >
        <option value="">All Status</option>
        <option value="New">New</option>
        <option value="InProgress">In Progress</option>
        <option value="Blocked">Blocked</option>
        <option value="Completed">Completed</option>
      </select>
    </div>

    <CreateWorkRequestForm onCreated={loadRequests} />

    {loading ? (
      <p className="loading">Loading...</p>
    ) : (
      <WorkRequestList requests={requests} onRefresh={loadRequests} />
    )}
  </div>
  );
}

export default App;