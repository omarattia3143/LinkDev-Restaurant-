import React, { useEffect, useState } from "react";
import Layout from "../components/Layout";
import axios from "axios";
import { User } from "../models/user";
import {
  Button,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableFooter,
  TableHead,
  TablePagination,
  TableRow,
} from "@mui/material";

const Users = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [page, setPage] = useState(0);
  const perPage = 10;

  useEffect(() => {
    (async () => {
      const { data } = await axios.get("/ambassadors");
      setUsers(data);
    })();
  }, []);

  return (
    <Layout>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>#</TableCell>
              <TableCell>Name</TableCell>
              <TableCell>Email</TableCell>
              <TableCell>Action</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {users.slice(page * perPage, (page + 1) * perPage).map((user) => {
              return (
                <TableRow key={user.id}>
                  <TableCell>{user.id}</TableCell>
                  <TableCell>
                    {user.first_name} {user.last_name}
                  </TableCell>
                  <TableCell>{user.email}</TableCell>
                  <TableCell>
                    <Button
                      variant="contained"
                      color="primary"
                      href={`users/${user.id}/links`}
                    >
                      View
                    </Button>
                  </TableCell>
                </TableRow>
              );
            })}
          </TableBody>
          <TableFooter>
            <TableRow>
              <TablePagination
                count={users.length}
                page={page}
                rowsPerPage={perPage}
                onPageChange={(e, newPage) => setPage(newPage)}
                rowsPerPageOptions={[]}
              />
            </TableRow>
          </TableFooter>
        </Table>
      </TableContainer>
    </Layout>
  );
};

export default Users;
