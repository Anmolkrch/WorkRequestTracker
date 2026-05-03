import axios from "axios";

const API_BASE_URL = "http://localhost:5130/api/work-requests";

export const getWorkRequests = async (status = "", search = "") => {
  const response = await axios.get(API_BASE_URL, {
    params: { status, search }
  });
  return response.data;
};

export const createWorkRequest = async (payload) => {
  const response = await axios.post(API_BASE_URL, payload);
  return response.data;
};

export const updateStatus = async (id, status) => {
  const response = await axios.patch(
    `${API_BASE_URL}/${id}/status`,
    { status }
  );
  return response.data;
};

export const addNote = async (id, note) => {
  const response = await axios.post(
    `${API_BASE_URL}/${id}/notes`,
    { note }
  );
  return response.data;
};