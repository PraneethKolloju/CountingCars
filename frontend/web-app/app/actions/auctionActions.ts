'use server';
import { auth } from "@/auth";
import { Auction, PagedResult } from "@/types";

export async function GetData(query: string): Promise<PagedResult<Auction>> {
    const result = await fetch(`http://localhost:6001/search${query}`);
    return result.json();
}


export async function UpdateAuction(): Promise<{ status: number, message: string }> {

    const session = await auth();
    const data = {
        mileage: Math.floor(Math.random() * 10000) + 1
    }

    const res = await fetch(`http://localhost:6001/auctions/bbab4d5a-8565-48b1-9450-5ac2a5c4a654`,
        {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${session?.accessToken}`
            },
            body: JSON.stringify(data)
        }
    );

    return { status: res.status, message: res.statusText }
}