'use server';
import { Auction, PagedResult } from "@/types";

export async function GetData(query: string): Promise<PagedResult<Auction>> {
    const result = await fetch(`http://localhost:6001/search${query}`);
    return result.json();
}