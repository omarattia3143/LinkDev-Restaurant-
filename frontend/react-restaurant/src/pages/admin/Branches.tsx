import * as React from 'react';
import {DataGrid, GridRowsProp, GridColDef, GridCellValue, GridApi} from '@mui/x-data-grid';
import Layout from "../../components/Layout";
import {useEffect, useState} from "react";
import axios from "axios";
import {Branch} from "../../models/Branch";
import {Button} from "@mui/material";
import authHeader from "../../Services/auth-header";



export default function App() {

    const columns: GridColDef[] = [
        {field: 'id', headerName: 'id', width: 150},
        {field: 'Title', headerName: 'Title', width: 150},
        {field: 'Manager', headerName: 'Manager', width: 150},
        {field: 'Opens', headerName: 'Opens', width: 150},
        {field: 'Closes', headerName: 'Closes', width: 150},
        {
            field: "Action",
            headerName: "Action",
            sortable: false,
            renderCell: (params) => {
                const onClick = async (e: any) => {
                    e.stopPropagation(); // don't select this row after clicking

                    const api: GridApi = params.api;
                    const thisRow: Record<string, GridCellValue> = {};

                    api.getAllColumns()
                        .filter((c) => c.field !== "__check__" && !!c)
                        .forEach(
                            (c) => (thisRow[c.field] = params.getValue(params.id, c.field))
                        );


                    // return alert(JSON.stringify(thisRow, null, 4));

                    // eslint-disable-next-line no-restricted-globals
                    if (confirm("Are you sure?")) {
                        await DeleteBranch(thisRow.id);
                    }
                };

                return <Button onClick={onClick}>Delete</Button>;
            }
        }
    ];



    const [branches, setBranches] = useState<Branch[]>([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [itemsPerPage, setItemsPerPage] = useState(3);
    const [totalItems, setTotalItems] = useState(3);
    useEffect(() => {
        (async () => {
            // @ts-ignore
            const {data, headers} = await axios.get("branch/GetAllBranches",{ headers: authHeader() });
            console.log(headers.pagination);

            const pagination = JSON.parse(headers.pagination);
            setCurrentPage(pagination.currentPage);
            setItemsPerPage(pagination.itemsPerPage);
            setTotalItems(pagination.totalItems);
            setItemsPerPage(pagination.itemsPerPage)
            setBranches(data);
        })();
    }, []);

    const rows = branches.map((row) => ({
        id: row.id,
        Title: row.title,
        Manager: row.managerName,
        Opens: row.openingTime.toString(),
        Closes: row.closingTime.toString(),

    }));


    async function DeleteBranch(id: any) {
        if (window.confirm("Are you sure?")) {

            await axios.delete(`branch/DeleteBranch/${id}`);
            // @ts-ignore
            const {data} = await axios.get("branch/GetAllBranches",{ headers: authHeader() });

            setBranches(data);
        }
    }

    const handlePageChange = async (pageIndex: number) => {
        const page = pageIndex + 1; //because pageIndex is zero based

        // @ts-ignore
        const {data, headers} = await axios.get("branch/GetAllBranches?PageNumber=" + page,{ headers: authHeader() });
        const pagination = JSON.parse(headers.pagination);
        setCurrentPage(pagination.currentPage);
        setItemsPerPage(pagination.itemsPerPage);
        setTotalItems(pagination.totalItems);
        setItemsPerPage(pagination.itemsPerPage)
        setBranches(data);

    };

    return (
        <Layout>
            <div className="pt-3 pb-2 mb-3 boarder-bottom">
                <Button
                    href={"/branch/create"}
                    variant={"contained"}
                    color={"primary"}
                >
                    Add
                </Button>
            </div>
            <div style={{height: 300, width: '100%'}}>
                <DataGrid
                    rows={rows}
                    columns={columns}
                    paginationMode={"server"}
                    rowsPerPageOptions={[]}
                    rowCount={totalItems}
                    pageSize={itemsPerPage}
                    onPageChange={handlePageChange}
                />
            </div>
        </Layout>
    );
}