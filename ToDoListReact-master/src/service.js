import axios from 'axios';

const config={
  apiUrl: process.env.REACT_APP_URL
}

export default {
  getTasks: async () => {
    const result = await axios.get(`${config.apiUrl}/items`)    
    return result.data;
  },

  addTask: async(name)=>{
    console.log('addTask', name)
    //TODO
    return {};
  },

  setCompleted: async(id, isComplete)=>{
    console.log('setCompleted', {id, isComplete})
    //TODO
    return {};
  },

  deleteTask:async()=>{
    console.log('deleteTask')
  }
};
